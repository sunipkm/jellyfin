#nullable disable
#pragma warning disable CS1591

using System;

namespace MediaBrowser.Model.Dlna
{
    public static class ResolutionNormalizer
    {
        private static readonly ResolutionConfiguration[] Configurations =
            new[]
            {
                 new ResolutionConfiguration(426, 420000),
                 new ResolutionConfiguration(640, 420001),
                 new ResolutionConfiguration(720, 600000),
                 new ResolutionConfiguration(1280, 750001),
                 new ResolutionConfiguration(1920, 1500001),
                 new ResolutionConfiguration(2560, 3000001),
                 new ResolutionConfiguration(3840, 4000001)
            };

        public static ResolutionOptions Normalize(
            int? inputBitrate,
            int outputBitrate,
            int? maxWidth,
            int? maxHeight)
        {
            // If the bitrate isn't changing, then don't downscale the resolution
            if (inputBitrate.HasValue && outputBitrate >= inputBitrate.Value)
            {
                if (maxWidth.HasValue || maxHeight.HasValue)
                {
                    return new ResolutionOptions
                    {
                        MaxWidth = maxWidth,
                        MaxHeight = maxHeight
                    };
                }
            }

            var resolutionConfig = GetResolutionConfiguration(outputBitrate);
            if (resolutionConfig is not null)
            {
                var originvalValue = maxWidth;

                maxWidth = Math.Min(resolutionConfig.MaxWidth, maxWidth ?? resolutionConfig.MaxWidth);
                if (!originvalValue.HasValue || originvalValue.Value != maxWidth.Value)
                {
                    maxHeight = null;
                }
            }

            return new ResolutionOptions
            {
                MaxWidth = maxWidth,
                MaxHeight = maxHeight
            };
        }

        private static ResolutionConfiguration GetResolutionConfiguration(int outputBitrate)
        {
            ResolutionConfiguration previousOption = null;

            foreach (var config in Configurations)
            {
                if (outputBitrate <= config.MaxBitrate)
                {
                    return previousOption ?? config;
                }

                previousOption = config;
            }

            return null;
        }
    }
}
