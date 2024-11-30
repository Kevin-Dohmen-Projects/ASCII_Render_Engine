namespace ASCII_Render_Engine.Core
{
    public struct ScreenConfig
    {
        // Resolution:
        public bool ScaleToWindow;

        // Render settings:
        public bool Dithering;
        public double FPSCap;
        public bool CenterScreen;

        public bool VisualizeAsync;

        public ScreenConfig()
        {
            ScaleToWindow = false;

            Dithering = false;
            FPSCap = 30;
            CenterScreen = false;

            VisualizeAsync = false;
        }
    }
}
