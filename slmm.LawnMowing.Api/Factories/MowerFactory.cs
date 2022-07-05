namespace slmm.LawnMowing.Api.Factories
{
    using System;
    using SmartLawnMowing.Domain.Model;
    using Service;

    public class MowerFactory: IMowerFactory
    {
        private readonly IAppSettingsProvider settingsProvider;
        private Mower Instance;

        public const int MowerDefaultXPosition = 1;
        public const int MowerDefaultYPosition = 1;
        public const string MowerDefaultOrientation = "East";
        public const int DefaultLength = 5;
        public const int DefaultWidth = 5;

        public MowerFactory(IAppSettingsProvider settingsProvider)
        {
            this.settingsProvider = settingsProvider;
        }

        public Mower Create()
        {
            if (this.Instance == null)
            {
                var length = this.settingsProvider.AppSettingWithFallBack<int>("Dimensions", "length", DefaultLength);
                var width = this.settingsProvider.AppSettingWithFallBack<int>("Dimensions", "width", DefaultWidth);
                
                var initialX = this.settingsProvider.AppSettingWithFallBack<int>("MowerSettings", "XCoOrdinates", MowerDefaultXPosition);
                var initialY = this.settingsProvider.AppSettingWithFallBack<int>("MowerSettings", "YCoOrdinates", MowerDefaultYPosition);
                var initialOrientation = this.settingsProvider.AppSettingWithFallBack<string>("MowerSettings", "orientation", MowerDefaultOrientation);
               
                Enum.TryParse<Direction>(initialOrientation, out var orientation);
                var mower = new Mower(new Garden(length, width), new Position(new Coordinates(initialX, initialY), orientation));
                this.Instance = mower;
            }

            return this.Instance;
        }
    }
}
