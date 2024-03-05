namespace TeapotPlugin.Wrapper
{
    using TeapotPlugin.Model;

    public class TeapotBuilder
    {
        /// <summary>
        /// Instance of the class Kompas3DWrapper.
        /// </summary>
        private readonly Kompas3DWrapper _kompas3DWrapper = new Kompas3DWrapper();

        /// <summary>
        /// Build teapot
        /// </summary>
        /// <param name="photoFrameParameters">getParameterByType for build the teapot</param>
        public void BuildTeapot(TeapotParameters photoFrameParameters)
        {
            _kompas3DWrapper.OpenKompas();
            _kompas3DWrapper.CreateDocument3D();
            _kompas3DWrapper.CreatePart();

            _kompas3DWrapper.CreateTeapotsBody(photoFrameParameters.getParameterByType(ParameterType.Height).Value,
                                               photoFrameParameters.getParameterByType(ParameterType.BaseCircle).Value);

            _kompas3DWrapper.CreateTeapotsHandle(photoFrameParameters.getParameterByType(ParameterType.Height).Value,
                                                 photoFrameParameters.getParameterByType(ParameterType.BaseCircle).Value,
                                                 photoFrameParameters.getParameterByType(ParameterType.HandleThickness).Value,
                                                 photoFrameParameters.getParameterByType(ParameterType.HandleType).Value);

            _kompas3DWrapper.CreateSpout(photoFrameParameters.getParameterByType(ParameterType.SpoutLength).Value,
                                         photoFrameParameters.getParameterByType(ParameterType.Height).Value,
                                         photoFrameParameters.getParameterByType(ParameterType.BaseCircle).Value,
                                         photoFrameParameters.getParameterByType(ParameterType.OuterSpoutCircle).Value,
                                         photoFrameParameters.getParameterByType(ParameterType.InnerSpoutCircle).Value);
        }
    }
}