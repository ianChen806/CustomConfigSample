using System;
using Microsoft.Extensions.Configuration;

namespace CustomConfigSample
{
    public class MyConfigSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new MyConfigProvider();
        }
    }

    public class MyConfigProvider : ConfigurationProvider
    {
        public override void Load()
        {
            Data["Version"] = DateTime.Now.Ticks.ToString();

            ReloadData();
            
            OnReload();
        }

        private void ReloadData()
        {
            //any thing...
        }
    }
}