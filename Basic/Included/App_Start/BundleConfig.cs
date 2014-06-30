using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Optimization;

namespace Included
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        { 
            // 
            var scriptsBundle = new Bundle("~/Included/Scripts", new StringResourceTransform(new List<String>() 
                                                                                            { 
                                                                                                "Included.Scripts.test1.js" 
                                                                                            }));
#if(!DEBUG)
            scriptsBundle.Transforms.Add(new JsMinify());
#endif 
            bundles.Add(scriptsBundle);

            var cssBundle = new Bundle("~/Included/Content", new StringResourceTransform(new List<String>() 
                                                                                            { 
                                                                                               "Included.Content.Test.css" 
                                                                                            }, "text/css"));
#if(!DEBUG)
            cssBundle.Transforms.Add(new CssMinify());
#endif
            bundles.Add(cssBundle);

            BundleTable.EnableOptimizations = true;
        }
    }

    public class StringResourceTransform : IBundleTransform
    {
        private List<String> _resourceFiles = new List<String>();
        private string _contentType;

        public StringResourceTransform(List<String> resourceFiles, String contentType = "text/javascript")
        {
            _resourceFiles = resourceFiles;
            _contentType = contentType;
        }

        public void Process(BundleContext context, BundleResponse response)
        {
            string result = String.Empty;

            foreach (var resource in _resourceFiles)
            {
                using (Stream stream = Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream(resource))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        result += reader.ReadToEnd();
                    }
                }
            }

            response.ContentType = _contentType;
            response.Content = result;
        }
    }
}
