using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustOptimizer.Core
{
    public static class Optimizer
    {
        /// <summary>
        /// This method gets the right settings for a specific profile, like Competitive or Ultra.
        /// It's the main logic for applying all the tweaks.
        /// </summary>
        public static Dictionary<string, string> GetOptimalSettings(string profile)
        {
            var settings = new Dictionary<string, string>();

            // Universal Settings for all profiles
            settings.Add("graphics.vsync", "False");
            settings.Add("client.headbob", "False");
            settings.Add("effects.sharpen", "False");
            settings.Add("effects.motionblur", "False");
            settings.Add("effects.antialiasing", "0");
            settings.Add("effects.maxgibs", "0");
            settings.Add("global.showblood", "True");
            settings.Add("gc.buffer", "4096");
            settings.Add("client.clampscreenshake", "True");
            settings.Add("effects.showoutlines", "False");
            settings.Add("graphics.impostorshadows", "False");
            settings.Add("console.erroroverlay", "False");
            settings.Add("accessibility.treemarkercolor", "0");
            settings.Add("audio.musicvolume", "0");
            settings.Add("audio.musicvolumemenu", "0");

            switch (profile)
            {
                case "Competitive (Max FPS)":
                    // Pure performance focus based on the latest video
                    settings.Add("graphics.drawdistance", "500");
                    settings.Add("graphics.lodbias", "1");
                    settings.Add("graphics.af", "1");
                    settings.Add("graphics.shadowquality", "0");
                    settings.Add("graphicssettings.shadowcascades", "0");
                    settings.Add("tree.meshes", "0");
                    settings.Add("effects.ao", "False");
                    settings.Add("graphics.dof", "False");
                    settings.Add("effects.bloom", "False");
                    settings.Add("effects.shafts", "False");
                    settings.Add("effects.vignet", "False");
                    settings.Add("grass.quality", "0");
                    settings.Add("particle.quality", "0");
                    settings.Add("sss.enabled", "False");
                    settings.Add("water.quality", "0");
                    settings.Add("water.reflections", "0");
                    break;

                case "Recommended (Optimized)":
                    // Balances performance with visual fidelity to reduce pop-in.
                    settings.Add("graphics.drawdistance", "1500");
                    settings.Add("graphics.lodbias", "2.0");
                    settings.Add("graphics.af", "4");
                    settings.Add("graphics.shadowquality", "1");
                    settings.Add("graphicssettings.shadowcascades", "1");
                    settings.Add("tree.meshes", "50");
                    settings.Add("effects.ao", "True");
                    settings.Add("graphics.dof", "False");
                    settings.Add("effects.bloom", "True");
                    settings.Add("effects.shafts", "True");
                    settings.Add("effects.vignet", "False");
                    settings.Add("grass.quality", "50");
                    settings.Add("particle.quality", "50");
                    settings.Add("sss.enabled", "True");
                    settings.Add("water.quality", "1");
                    settings.Add("water.reflections", "1");
                    break;

                case "Balanced (Good-looking & Fast)":
                    // A middle ground that provides a good experience on most systems.
                    settings.Add("graphics.drawdistance", "1000");
                    settings.Add("graphics.lodbias", "1.5");
                    settings.Add("graphics.af", "2");
                    settings.Add("graphics.shadowquality", "1");
                    settings.Add("graphicssettings.shadowcascades", "1");
                    settings.Add("tree.meshes", "30");
                    settings.Add("effects.ao", "True");
                    settings.Add("graphics.dof", "False");
                    settings.Add("effects.bloom", "True");
                    settings.Add("effects.shafts", "True");
                    settings.Add("effects.vignet", "False");
                    settings.Add("grass.quality", "25");
                    settings.Add("particle.quality", "25");
                    settings.Add("sss.enabled", "True");
                    settings.Add("water.quality", "1");
                    settings.Add("water.reflections", "1");
                    break;

                case "Ultra (Maximum Visuals)":
                    // High-quality settings based on the provided client.cfg
                    settings.Add("graphics.drawdistance", "2500");
                    settings.Add("graphics.lodbias", "5");
                    settings.Add("graphics.af", "16");
                    settings.Add("graphics.shadowquality", "0");
                    settings.Add("graphicssettings.shadowcascades", "1");
                    settings.Add("tree.meshes", "100");
                    settings.Add("effects.ao", "False");
                    settings.Add("graphics.dof", "False");
                    settings.Add("effects.bloom", "False");
                    settings.Add("effects.shafts", "True");
                    settings.Add("effects.vignet", "False");
                    settings.Add("grass.quality", "0");
                    settings.Add("particle.quality", "0");
                    settings.Add("sss.enabled", "True");
                    settings.Add("water.quality", "0");
                    settings.Add("water.reflections", "0");
                    break;

                default:
                    // Fallback to Recommended (Optimized) as the default
                    goto case "Recommended (Optimized)";
            }
            return settings;
        }
    }
}