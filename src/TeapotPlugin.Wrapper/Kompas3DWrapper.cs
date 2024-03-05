namespace TeapotPlugin.Wrapper
{
    using Kompas6API5;
    using Kompas6Constants3D;
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    public class Kompas3DWrapper
    {
        /// <summary>
        /// Kompas object
        /// </summary>
        private KompasObject _kompasObject { get; set; }

        /// <summary>
        /// 3D document
        /// </summary>
        private ksDocument3D _document3D { get; set; }

        /// <summary>
        /// Detail
        /// </summary>
        private ksPart _part { get; set; }

        /// <summary>
        /// 2D document.
        /// </summary>
        private ksDocument2D _document2D { get; set; }

        /// <summary>
        /// Open Kompas
        /// </summary>
        public void OpenKompas()
        {
            Process[] pname = Process.GetProcessesByName("kStudy");
            if (pname.Length != 0)
            {
                _kompasObject = (KompasObject)Marshal.GetActiveObject("KOMPAS.Application.5");
                _kompasObject.ActivateControllerAPI();
            }
            else
            {
                _kompasObject = null;
                Type type = Type.GetTypeFromProgID("KOMPAS.Application.5");
                _kompasObject = (KompasObject)Activator.CreateInstance(type);
                _kompasObject.Visible = true;
                _kompasObject.ActivateControllerAPI();
            }
        }

        /// <summary>
        /// Create 3D document
        /// </summary>
        public void CreateDocument3D()
        {
            _document3D = (ksDocument3D)_kompasObject.Document3D();
            _document3D.Create(false /*видимый*/, true /*деталь*/);
        }

        /// <summary>
        /// Create detail.
        /// </summary>
        public void CreatePart()
        {
            _part = _document3D.GetPart((short)Part_Type.pTop_Part);
        }

        /// <summary>
        /// Create teapot body
        /// </summary>
        /// <param name="height"> height of the teapot </param>
        /// <param name="baseCircle"> baseCircle of the teapot </param>
        /// <returns></returns>
        public ksEntity CreateTeapotsBody(double height, double baseCircle)
        {
            ksEntity sketch = TeapotsBodySketch(height, baseCircle);
            BossRotatedExtrusion(sketch, 20);
            return sketch;
        }

        /// <summary>
        /// Create teapot handle
        /// </summary>
        /// <param name="height"> height of the teapot </param>
        /// <param name="baseCircle"> Radius of the base circle of the teapot </param>
        /// <param name="handleThickness">Radius of the handle thickness</param>
        public void CreateTeapotsHandle(double height, double baseCircle, 
                                        double handleThickness, double handleType)
        {
            //Calculate the coordinates of the beginning and ending of the handle sketch 
            double circleRadius = Math.Sqrt((height / 2 * height / 2) + (baseCircle * baseCircle));
            double xStart = Math.Sqrt((circleRadius * circleRadius) - (height * 0.4 * height * 0.4));
            double yStart = (height * 0.4) + (height / 2);
            double yEnd = (height - yStart);

            ksEntity handleScketch = CreateHandleSketch(height, xStart, yStart, yEnd);
            ksEntity exstrusionScetch = SketchForSqueezingHandle(xStart, yStart, height, 
                                                                 baseCircle, handleThickness,
                                                                 handleType);
            KinematicExstrusion(exstrusionScetch, handleScketch);
        }

        /// <summary>
        /// Create teapot spout
        /// </summary>
        /// <param name="spoutLength">length of the spout</param>
        /// <param name="height"> Height of the teapot </param>
        /// <param name="baseCircle"> Radius of the base circle of the teapot </param>
        /// <param name="outerSpoutCircle"> Radius of the outer circle of the spout </param>
        /// <param name="innerSpoutCircle"> Radius of the inner circle of the spout </param>
        public void CreateSpout(double spoutLength, double height, double baseCircle, 
                                double outerSpoutCircle, double innerSpoutCircle)
        {
            ksEntity plane1 = CreatePlaneOffset(baseCircle, true);

            double angleInDegrees2 = 95;
            double offset2 = baseCircle + spoutLength;
            ksEntity plane2 = CreatePlaneOffset(offset2, true);
            ksAxis2PointsDefinition AxisDefinition2 = CreateAxisBy2Points(plane2,
                                                                          -25, -height,
                                                                           25, -height);
            plane2 = CreatePlaneAngle(plane2, AxisDefinition2, 360 - angleInDegrees2);

            double p1x = offset2;
            double p1y = height;

            // Сalculate the center of the new plane
            double theta = offset2 * GetSin(90 - angleInDegrees2) / GetSin(angleInDegrees2);
            double firstPointHypotenuse = height + (offset2 * GetSin(90 - angleInDegrees2) / GetSin(angleInDegrees2));
            double AddLine = (firstPointHypotenuse * GetSin(90 - angleInDegrees2) / GetSin(angleInDegrees2));
            double SecondPointHypotenuse = Math.Sqrt((offset2 * offset2) + (theta * theta));
            double Xcenter = AddLine - SecondPointHypotenuse;

            ksEntity directionalLineScketch = CreateSketchOfTheSpoutOpening(-baseCircle, height / 2,
                                                                            -p1x, p1y);
            directionalLineScketch.hidden = true;

            ksEntity sketch1 = DrawCircle(plane1, 0, -height / 2, 20 + (height * 0.15) );
            sketch1.hidden = true;

            ksEntity sketch2 = DrawCircle(plane2, Xcenter - (height * 0.03), 0, outerSpoutCircle);
            sketch2.hidden = true;

            CreateBossLoftDefinition(sketch1, sketch2, directionalLineScketch);
            ksEntity sketch3 = DrawCircle(plane2, Xcenter - (height * 0.03), 0, innerSpoutCircle);
            sketch3.hidden = true;

            CreateCutLoftDefinition(sketch1, sketch3, directionalLineScketch);
            ksEntity CutRotatedSketch = CreateCutRotatedSketch(height, baseCircle);
            CutRotatedSketch.hidden = true;

            CutRotated(CutRotatedSketch);
        }

        /// <summary>
        /// Create sketch of the teapot handle
        /// </summary>
        /// <param name="height"> Height of the teapot </param>
        /// <param name="xStart">X coordinate of the beginning of the creation of the arc of the kettle handle</param>
        /// <param name="yStart">Y coordinate of the beginning of the creation of the arc of the kettle handle</param>
        /// <param name="yEnd"> Y coordinate of the end of the creation of the arc of the kettle handle </param>
        /// <returns></returns>
        private ksEntity CreateHandleSketch(double height, double xStart, 
                                            double yStart, double yEnd)
        {
            ksEntity Sketch = _part.NewEntity((short)ksObj3dTypeEnum.o3d_sketch);
            ksSketchDefinition DefinitionSketch = Sketch.GetDefinition();
            DefinitionSketch.SetPlane(_part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY));
            Sketch.hidden = true;

            Sketch.Create();
            _document2D = DefinitionSketch.BeginEdit();
            _document2D.ksArcBy3Points(xStart - 3, yStart,
                                      xStart + (0.2 * height), yStart + (0.071 * height),
                                      xStart + (0.3 * height), yStart,
                                      1);

            _document2D.ksArcBy3Points(xStart + (0.3 * height), yStart,
                                      xStart + (0.4 * height), yStart - (0.3 * height),
                                      xStart + (0.33 * height), yStart - (0.6 * height),
                                      1);

            _document2D.ksArcBy3Points(xStart + (0.33 * height), yStart - (0.6 * height),
                                      xStart + (0.2 * height), yEnd + (0.07 * height),
                                      xStart - 3, yEnd,
                                      1);

            Sketch.Update();
            return Sketch;
        }

        /// <summary>
        /// Create cut rotated sketch
        /// </summary>
        /// <param name="height"> Height of the teapot </param>
        /// <param name="baseCircle"> Radius of the base circle of the teapot </param>
        /// <returns></returns>
        private ksEntity CreateCutRotatedSketch(double height, double baseCircle)
        {
            ksEntity Sketch = _part.NewEntity((short)ksObj3dTypeEnum.o3d_sketch);
            ksSketchDefinition DefinitionSketch = Sketch.GetDefinition();
            DefinitionSketch.SetPlane(_part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY));

            double circleRadius = Math.Sqrt((height / 2 * height / 2) + (baseCircle * baseCircle));

            Sketch.Create();
            _document2D = DefinitionSketch.BeginEdit();
            int circleSketch = _document2D.ksCircle(0, height / 2, circleRadius, 1);
            int curve = _document2D.ksTrimmCurve(circleSketch, baseCircle, 0, baseCircle, height, baseCircle, height, 1);
            EquidistantParam Equidistant = _kompasObject.GetParamStruct(25);
            Equidistant.radRight = 10;
            Equidistant.radLeft = 10;
            Equidistant.side = 0;
            Equidistant.style = 1;
            Equidistant.geoObj = curve;
            ksMathematic2D Mathematic2D = _kompasObject.GetMathematic2D();
            _document2D.ksEquidistant(Equidistant);
            _document2D.ksDeleteObj(curve);

            int line = _document2D.ksLineSeg(baseCircle, height, 0, height / 2, 1);
            ksDynamicArray pointArray = Mathematic2D.ksPointsOnCurveByStep(line, 10);
            ksMathPointParam pointParam = _kompasObject.GetParamStruct(14);
            pointArray.ksGetArrayItem(1, pointParam);

            _document2D.ksDeleteObj(line);
            _document2D.ksLineSeg(0, height / 2, pointParam.x, pointParam.y, 1);
            _document2D.ksLineSeg(0, height / 2, 0, height * 0.06, 3);

            _document2D.ksArcBy3Points(pointParam.x, height - pointParam.y,
                                      baseCircle / 2, height - pointParam.y + 3,
                                      0, height * 0.06,
                                      1);

            Sketch.hidden = true;
            Sketch.Update();
            return Sketch;
        }

        /// <summary>
        /// 360 degree rotation cutout.
        /// The sketch must have one centerline
        /// </summary>
        /// <param name="part">Assembly Component</param>
        /// <param name="sketch">Cutout sketch</param>
        private void CutRotated(ksEntity sketch)
        {
            ksEntity rotate =
                (ksEntity)_part.NewEntity((short)Obj3dType.o3d_cutRotated);
            ksCutRotatedDefinition rotDef =
                (ksCutRotatedDefinition)rotate.GetDefinition();

            rotDef.directionType = (short)Direction_Type.dtNormal;
            rotDef.cut = true;
            rotDef.SetSideParam(true, 360);
            rotDef.toroidShapeType = false;
            rotDef.SetSketch(sketch);
            rotate.Create();
        }

        /// <summary>
        /// Сutting a hole for the spout of the teapot.
        /// </summary>
        /// <param name="sketch1">First sketch of cutout</param>
        /// <param name="sketch2">Second sketch of cutout</param>
        /// <param name="directionalLine">Centerline</param>
        private void CreateCutLoftDefinition(ksEntity sketch1,
                                             ksEntity sketch2,
                                             ksEntity directionalLine)
        {
            ksEntity CutLoft = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_cutLoft);
            ksCutLoftDefinition CutLoftDefinition = CutLoft.GetDefinition();

            ksEntityCollection entityCollection = (ksEntityCollection)CutLoftDefinition.Sketchs();
            entityCollection.Add(sketch1);
            entityCollection.Add(sketch2);
            CutLoftDefinition.SetDirectionalLine(directionalLine);
            CutLoft.Create();
        }

        /// <summary>
        ///  Create extrusion for the spout of the teapot.
        /// </summary>
        /// <param name="sketch1"></param>
        /// <param name="sketch2"></param>
        /// <param name="directionalLine"></param>
        private void CreateBossLoftDefinition(ksEntity sketch1,
                                              ksEntity sketch2,
                                              ksEntity directionalLine)
        {
            ksEntity BossLoft = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_bossLoft);
            ksBossLoftDefinition BossLoftDef = (ksBossLoftDefinition)BossLoft.GetDefinition();
            
            ksEntityCollection entityCollection = (ksEntityCollection)BossLoftDef.Sketchs();
            entityCollection.Add(sketch1);
            entityCollection.Add(sketch2);
            BossLoftDef.SetDirectionalLine(directionalLine);
            BossLoft.Create();
        }

        /// <summary>
        /// Draw circle on the plane.
        /// </summary>
        /// <param name="plane">The plane for drawing</param>
        /// <param name="centerX">X coordinate of the center of the circle</param>
        /// <param name="centerY">Y coordinate of the center of the circle</param>
        /// <param name="radius"> The radius of the cirlce</param>
        /// <returns></returns>
        private ksEntity DrawCircle(ksEntity plane,
                                    double centerX, 
                                    double centerY,
                                    double radius)
        {
            ksEntity Sketch = _part.NewEntity((short)ksObj3dTypeEnum.o3d_sketch);
            ksSketchDefinition DefinitionSketch = Sketch.GetDefinition();
            DefinitionSketch.SetPlane(plane);

            Sketch.Create();
            _document2D = DefinitionSketch.BeginEdit();
            _document2D.ksCircle(centerX, centerY, radius, 1);

            Sketch.hidden = true;
            Sketch.Update();
            return Sketch;
        }

        /// <summary>
        /// Create arc sketch for the spout
        /// </summary>
        /// <param name="x1">X coordinate of the beginning of the creation of the arc sketch of the spout</param>
        /// <param name="y1">Y coordinate of the beginning of the creation of the arc sketch of the spout</param>
        /// <param name="x2">X coordinate of the end of the creation of the arc sketch of the spout</param>
        /// <param name="y2">Y coordinate of the end of the creation of the arc sketch of the spout</param>
        /// <returns></returns>
        private ksEntity CreateSketchOfTheSpoutOpening(double x1, double y1,
                                                       double x2, double y2)
        {
            ksEntity Sketch = _part.NewEntity((short)ksObj3dTypeEnum.o3d_sketch);
            ksSketchDefinition DefinitionSketch = Sketch.GetDefinition();
            DefinitionSketch.SetPlane(_part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY));
            Sketch.hidden = true;

            Sketch.Create();
            _document2D = DefinitionSketch.BeginEdit();
            _document2D.ksArcBy3Points(x1, y1, x1 - ((x1-x2)*0.14), y1, x2, y2, 1);

            Sketch.Update();
            return Sketch;
        }

        /// <summary>
        /// Converts degrees to the sine of an angle
        /// </summary>
        /// <param name="angleInDegrees">The angle in degrees</param>
        /// <returns> 
        /// returns the sine of the angle 
        /// </returns>
        private double GetSin(double angleInDegrees)
        {
            double radians = angleInDegrees * Math.PI / 180;
            return Math.Sin(radians);
        }

        /// <summary>
        /// Create axis by 2 points
        /// </summary>
        /// <param name="plane">The plane in which to build the axis</param>
        /// <param name="x1">X coordinate of the first point</param>
        /// <param name="y1">Y coordinate of the first point</param>
        /// <param name="x2">X coordinate of the second point</param>
        /// <param name="y2">Y coordinate of the second point</param>
        /// <returns>
        /// returns axis entity
        /// </returns>
        private ksAxis2PointsDefinition CreateAxisBy2Points(ksEntity plane,
                                                            double x1, double y1,
                                                            double x2, double y2)
        {
            ksEntity Sketch = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition DefinitionSketch = Sketch.GetDefinition();

            DefinitionSketch.SetPlane(plane);
            Sketch.hidden = true;
            Sketch.Create();
            _document2D = DefinitionSketch.BeginEdit();
            _document2D.ksPoint(x1, y1, 1);
            _document2D.ksPoint(x2, y2, 1);

            Sketch.Update();

            ksEntityCollection entityCollection = (ksEntityCollection)_part.EntityCollection((short)Obj3dType.o3d_vertex);
            ksEntity vertex1 = (ksEntity)entityCollection.GetByIndex(entityCollection.GetCount() - 1);
            ksEntity vertex2 = (ksEntity)entityCollection.GetByIndex(entityCollection.GetCount() - 2);

            ksVertexDefinition p1 = (ksVertexDefinition)vertex1.GetDefinition();

            double px;
            double py;
            double pz;
            p1.GetPoint(out px, out py, out pz);

            int vertex1PointNumber1;
            int vertex2PointNumber1;
            if (pz.ToString() == x2.ToString())
            {
                vertex1PointNumber1 = 2;
                vertex2PointNumber1 = 1;
            }
            else
            {
                vertex1PointNumber1 = 1;
                vertex2PointNumber1 = 2;
            }

            ksEntity Axis = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_axis2Points);
            ksAxis2PointsDefinition AxisDefinition = Axis.GetDefinition();
            AxisDefinition.SetPoint(vertex1PointNumber1, vertex1);
            AxisDefinition.SetPoint(vertex2PointNumber1, vertex2);
            Axis.hidden = true;
            Axis.Create();

            return AxisDefinition;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">X coordinate of the circle in XOY plane</param>
        /// <param name="y">Y coordinate of the circle in XOY plane</param>
        /// <param name="height"> Height of the teapot </param>
        /// <param name="baseCircle"> Radius of the base circle of the teapot </param>
        /// <param name="handleThickness">Radius of the handle thickness</param>
        /// <param name="handleType">type shaped handle</param>
        /// <returns>
        /// returns ksEntity of the handle circle sketch
        /// </returns>
        private ksEntity SketchForSqueezingHandle(double x, double y, double height,
                                                  double baseCircle, double handleThickness,
                                                  double handleType)
        {
            ksEntity Plane = CreatePlaneOffset(-x + 3, true);
            ksEntity Sketch = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_sketch);
            
            double angle = 90 - AngleBetween2vectors(0, height / 2,
                                                     x, y,
                                                     0, y);

            ksAxis2PointsDefinition AxisDefinition = CreateAxisBy2Points(Plane,
                                                                         -40, -y,
                                                                          40, -y);
            ksEntity PlaneAngle = CreatePlaneAngle(_part.GetDefaultEntity((short)Obj3dType.o3d_planeYOZ),
                                                   AxisDefinition,
                                                   angle);
            ksSketchDefinition DefinitionSketch = Sketch.GetDefinition();

            DefinitionSketch.SetPlane(PlaneAngle);
            Sketch.Create();
            _document2D = DefinitionSketch.BeginEdit();

            if (handleType == 0)
            {
                _document2D.ksCircle(-height * (56 - (7.2 * (height / baseCircle))) / 100,
                                    0, handleThickness, 1);
            }
            else
            {
                _document2D.ksLineSeg((-height * (56 - (7.2 * (height / baseCircle))) / 100) - handleThickness, handleThickness,
                                      (-height * (56 - (7.2 * (height / baseCircle))) / 100) - handleThickness, -handleThickness,
                                      1);
                _document2D.ksLineSeg((-height * (56 - (7.2 * (height / baseCircle))) / 100) - handleThickness, handleThickness,
                                      (-height * (56 - (7.2 * (height / baseCircle))) / 100) + handleThickness, handleThickness,
                                      1);
                _document2D.ksLineSeg((-height * (56 - (7.2 * (height / baseCircle))) / 100) - handleThickness, -handleThickness,
                                      (-height * (56 - (7.2 * (height / baseCircle))) / 100) + handleThickness, -handleThickness,
                                      1);
                _document2D.ksLineSeg((-height * (56 - (7.2 * (height / baseCircle))) / 100) + handleThickness, handleThickness,
                                      (-height * (56 - (7.2 * (height / baseCircle))) / 100) + handleThickness, -handleThickness,
                                      1);
            }

            Sketch.hidden = true;
            Sketch.Update();

            return Sketch;

        }

        /// <summary>
        /// Create new planeOffset
        /// </summary>
        /// <param name="offset">offset of the new plane</param>
        /// <param name="direction">direction of the new plane</param>
        /// <returns>
        /// returns ksEntity of the new plane
        /// </returns>
        private ksEntity CreatePlaneOffset(double offset, bool direction)
        {
            ksEntity Plane = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition PlaneDefinition = (ksPlaneOffsetDefinition)Plane.GetDefinition();
            PlaneDefinition.SetPlane(_part.GetDefaultEntity((short)Obj3dType.o3d_planeYOZ));
            PlaneDefinition.direction = direction;
            PlaneDefinition.offset = offset;
            Plane.hidden = true;
            Plane.Create();

            return Plane;
        }

        /// <summary>
        /// Create new PlaneAngle
        /// </summary>
        /// <param name="plane">base plane</param>
        /// <param name="axis">axis for new plane</param>
        /// <param name="angle">angle of the new plane</param>
        /// <returns>
        /// returns ksEntity of the new plane
        /// </returns>
        private ksEntity CreatePlaneAngle(object plane, object axis, double angle)
        {
            ksEntity PlaneAngle = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_planeAngle);
            ksPlaneAngleDefinition PlaneAngleDefinition = (ksPlaneAngleDefinition)PlaneAngle.GetDefinition();
            PlaneAngleDefinition.SetPlane(plane);
            PlaneAngleDefinition.SetAxis(axis);
            PlaneAngleDefinition.angle = angle;
            PlaneAngle.hidden = true;
            PlaneAngle.Create();
            return PlaneAngle;
        }

        /// <summary>
        /// Extrusion along the trajectory
        /// </summary>
        /// <param name="entityEvolution">kinematic cutout</param>
        /// <param name="skechOne">Section sketch</param>
        /// <param name="skechTwo">Sketch of the trajectory</param>
        /// <param name="ksEntityCollection"></param>
        private void KinematicExstrusion(ksEntity skechOne,
                                         ksEntity skechTwo)
        {
            ksEntity entityEvolution =
                (ksEntity)_part.NewEntity((short)Obj3dType.o3d_baseEvolution);
            ksBaseEvolutionDefinition baseEvolutionDefinition =
                (ksBaseEvolutionDefinition)entityEvolution.GetDefinition();

            baseEvolutionDefinition.sketchShiftType = 1;
            baseEvolutionDefinition.SetSketch(skechOne);
            ksEntityCollection ksEntityCollection =
                (ksEntityCollection)baseEvolutionDefinition.PathPartArray();

            ksEntityCollection.Clear();
            ksEntityCollection.Add(skechTwo);
            entityEvolution.Create();
        }

        /// <summary>
        /// 360 degree cutout
        /// </summary>
        /// <param name="sketch">sketch of the cutout</param>
        /// <param name="type">cutout type</param>
        private void BossRotatedExtrusion(ksEntity sketch, short type)
        {
            ksEntity bossRotated = _part.NewEntity((short)Obj3dType.o3d_bossRotated);
            ksBossRotatedDefinition bossRotatedDef = bossRotated.GetDefinition();

            bossRotatedDef.directionType = type;
            bossRotatedDef.SetSketch(sketch);
            bossRotatedDef.SetSideParam(true, 360);

            bossRotated.Create();
        }

        /// <summary>
        /// Creating a truncated sphere.
        /// </summary>
        /// <returns>
        /// returns ksEntity of the new teapots body sketch
        /// </returns>
        private ksEntity TeapotsBodySketch(double height, double baseCircle)
        {
            ksEntity Sketch = _part.NewEntity((short)ksObj3dTypeEnum.o3d_sketch);
            ksSketchDefinition DefinitionSketch = Sketch.GetDefinition();
            DefinitionSketch.SetPlane(_part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY));

            double circleRadius = Math.Sqrt((height / 2 * height / 2) + (baseCircle * baseCircle));

            Sketch.Create();
            _document2D = DefinitionSketch.BeginEdit();
            _document2D.ksLineSeg(0, 0, 0, height * 0.06, 3);
            _document2D.ksLineSeg(0, 0, baseCircle, 0, 1);
            int circleSketch = _document2D.ksCircle(0, height / 2, circleRadius, 1);
            int curve = _document2D.ksTrimmCurve(circleSketch, baseCircle, 0, baseCircle, height, baseCircle, height, 1);
            EquidistantParam Equidistant = _kompasObject.GetParamStruct(25);
            Equidistant.radRight = 10;
            Equidistant.radLeft = 10;
            Equidistant.side = 0;
            Equidistant.style = 1;
            Equidistant.geoObj = curve;
            ksMathematic2D Mathematic2D =  _kompasObject.GetMathematic2D();
            _document2D.ksEquidistant(Equidistant);
            _document2D.ksTrimmCurve(circleSketch, baseCircle, 0, baseCircle, height, baseCircle, height, 1);

            int line = _document2D.ksLineSeg(baseCircle, height, 0, height / 2, 1);
            ksDynamicArray pointArray = Mathematic2D.ksPointsOnCurveByStep(line, 10);
            ksMathPointParam pointParam = _kompasObject.GetParamStruct(14);
            pointArray.ksGetArrayItem(1, pointParam);

            _document2D.ksDeleteObj(line);
            _document2D.ksLineSeg(baseCircle, height, pointParam.x, pointParam.y, 1);

            _document2D.ksArcBy3Points(pointParam.x, height - pointParam.y,
                                      baseCircle / 2, height - pointParam.y + 3,
                                      0, height * 0.06,
                                      1);
            Sketch.hidden = true;
            Sketch.Update();

            return Sketch;
        }

        /// <summary>
        /// Calculate the angle between 2 vectors
        /// </summary>
        /// <param name="x1">X coordinate of the start point </param>
        /// <param name="y1">Y coordinate of the start point</param>
        /// <param name="x2">X coordinate of the endpoint of the first vector</param>
        /// <param name="y2">Y coordinate of the endpoint of the first vector</param>
        /// <param name="x3">X coordinate of the endpoint of the second vector</param>
        /// <param name="y3">Y coordinate of the endpoint of the second vector</param>
        /// <returns></returns>
        private double AngleBetween2vectors(double x1, double y1,
                                            double x2, double y2,
                                            double x3, double y3)
        {
            // Create vectors from the points
            double[] vectorA = { x2 - x1, y2 - y1 };
            double[] vectorB = { x3 - x1, y3 - y1 };

            // Calculate the dot product of the vectors
            double dotProduct = (vectorA[0] * vectorB[0]) + (vectorA[1] * vectorB[1]);

            // Calculate magnitudes of the vectors
            double magnitudeA = Math.Sqrt(Math.Pow(vectorA[0], 2) + Math.Pow(vectorA[1], 2));
            double magnitudeB = Math.Sqrt(Math.Pow(vectorB[0], 2) + Math.Pow(vectorB[1], 2));

            // Calculate the cosine of the angle between the vectors
            double cosAngle = dotProduct / (magnitudeA * magnitudeB);

            // Ensure the cosine value does not fall out of the range -1 to 1
            cosAngle = Math.Max(-1, Math.Min(1, cosAngle));

            // Calculate the angle in radians
            double angleRadians = Math.Acos(cosAngle);

            // Convert radians to degrees
            double angleDegrees = angleRadians * (180 / Math.PI);

            return angleDegrees;
        }
    }
}