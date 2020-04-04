using System;
using System.Threading.Tasks;
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
        public MyConfigProvider()
        {
            Task.Run(async () =>
            {
                while(true)
                {
                    await Task.Delay(1000 * 3);
                    LoadData(true);
                }
            });
        }

        public override void Load()
        {
            LoadData(false);
        }

        private void LoadData(bool reload)
        {
            //anything...
            Data["Version"] = DateTime.Now.Ticks.ToString();

            if (reload)
            {
                OnReload();
            }
        }
    }
}