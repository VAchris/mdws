using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using gov.va.medora.mdo;
using System.Configuration;
using gov.va.medora.mdo.conf;

namespace gov.va.medora.mdws.conf
{
    /// <summary>
    /// Provides a wrapper for MDWS configuration settings
    /// </summary>
    public class MdwsConfiguration : AppConfig
    {
        #region Private properties and their accessors

        FacadeConfiguration _facadeConfiguration;
        public FacadeConfiguration FacadeConfiguration
        {
            get { return _facadeConfiguration; }
            set { _facadeConfiguration = value; }
        }

        bool _applicationSessionsLogging;
        public bool ApplicationSessionsLogging
        {
            get { return _applicationSessionsLogging; }
            set { _applicationSessionsLogging = value; }
        }

        ApplicationSessionsLogLevel _applicationSessionsLogLevel;
        public ApplicationSessionsLogLevel ApplicationSessionsLogLevel
        {
            get { return _applicationSessionsLogLevel; }
            set { _applicationSessionsLogLevel = value; }
        }

        string _resourcesPath;
        /// <summary>
        /// Obtains the resources path on the host machine. Always ends with a '\' per mdo.Utils.FileUtils documentation
        /// </summary>
        /// <example>
        /// string sitesFilePath = myMdwsConfiguration.ResourcesPath + "xml\\" + sitesFileName;
        /// </example>
        public string ResourcesPath
        {
            get { return _resourcesPath; }
            set { _resourcesPath = value; }
        }

        /// <summary>
        /// The full path to the config file
        /// </summary>
        public string ConfigFilePath { get; set; }

        bool _isProduction;
        public bool IsProduction
        {
            get { return _isProduction; }
        }
        #endregion

        /// <summary>
        /// MDWS configuration settings wrapper class. This class fetches all the MDWS registry settings
        /// upon instantiation and builds its objects accordingly.
        /// </summary>
        public MdwsConfiguration() : base()
        {
            setResourcesPaths();
            base.readConfigFile(ConfigFilePath);
            setConfiguration(null);
        }

        /// <summary>
        /// MDWS configuration settings wrapper class. This class fetches all the MDWS registry settings
        /// upon instantiation and builds its objects accordingly.
        /// </summary>
        /// <param name="facadeName">The facade being invoked by a client application</param>
        public MdwsConfiguration(string facadeName) : base()
        {
            setResourcesPaths();
            base.readConfigFile(ConfigFilePath);
            setConfiguration(facadeName);
        }

        private void setResourcesPaths()
        {
            // set defaults in case no registry entries exist
            _resourcesPath = utils.ResourceUtils.ResourcesPath;
#if DEBUG
            if (System.IO.File.Exists(_resourcesPath + "conf\\secret-mdws.conf"))
            {
                ConfigFilePath = _resourcesPath + "conf\\secret-mdws.conf";
            }
            else
            {
                ConfigFilePath = _resourcesPath + "conf\\app.conf";
            }
#else
            ConfigFilePath = _resourcesPath + "conf\\app.conf";
#endif
        }

        private void setConfiguration(string facadeName)
        {
            setDefaults();

            if (base.AllConfigs == null || base.AllConfigs.Count == 0)
            {
                return; // no config items present
            }

            if (!String.IsNullOrEmpty(facadeName) && base.AllConfigs.ContainsKey(facadeName))
            {
                _facadeConfiguration = new FacadeConfiguration(base.AllConfigs[facadeName]);
            }

            if (base.AllConfigs.ContainsKey(MdwsConfigConstants.MDWS_CONFIG_SECTION))
            {
                if (base.AllConfigs[MdwsConfigConstants.MDWS_CONFIG_SECTION].ContainsKey(MdwsConfigConstants.SESSIONS_LOG_LEVEL))
                {
                    try
                    {
                        _applicationSessionsLogLevel = (ApplicationSessionsLogLevel)Enum.Parse(typeof(ApplicationSessionsLogLevel), 
                            base.AllConfigs[MdwsConfigConstants.MDWS_CONFIG_SECTION][MdwsConfigConstants.SESSIONS_LOG_LEVEL]);
                    }
                    catch (Exception) { }
                }
                if (base.AllConfigs[MdwsConfigConstants.MDWS_CONFIG_SECTION].ContainsKey(MdwsConfigConstants.SESSIONS_LOGGING))
                {
                    Boolean.TryParse(base.AllConfigs[MdwsConfigConstants.MDWS_CONFIG_SECTION][MdwsConfigConstants.SESSIONS_LOGGING], 
                        out _applicationSessionsLogging);
                }
            }
        }

        private void setDefaults()
        {
            _applicationSessionsLogging = false;
            _applicationSessionsLogLevel = mdws.ApplicationSessionsLogLevel.info;
            _isProduction = true;
            _facadeConfiguration = new FacadeConfiguration(null);

            if (base.AllConfigs == null || base.AllConfigs.Count == 0)
            {
                base.AllConfigs = new Dictionary<string, Dictionary<string, string>>();
            }
            if (!base.AllConfigs.ContainsKey(MdwsConfigConstants.MDWS_CONFIG_SECTION))
            {
                // MySession depends on these 3 config items - set to defaults
                Dictionary<string, string> mdwsConfigs = new Dictionary<string, string>();
                mdwsConfigs.Add(MdwsConfigConstants.DEFAULT_VISIT_METHOD, "NON-BSE CREDENTIALS");
                mdwsConfigs.Add(MdwsConfigConstants.EXCLUDE_SITE_200, "true");
                mdwsConfigs.Add(MdwsConfigConstants.DEFAULT_CONTEXT, "OR CPRS GUI CHART");
                base.AllConfigs.Add(MdwsConfigConstants.MDWS_CONFIG_SECTION, mdwsConfigs);
            }
        }

    }


    public class FacadeConfiguration
    {
        #region Private properties and their accessors
        private string _sitesFileName;
        public string SitesFileName
        {
            get { return _sitesFileName; }
            set { _sitesFileName = value; }
        }

        private bool _isProduction;
        public bool IsProduction
        {
            get { return _isProduction; }
        }

        private string _version;
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        #endregion

        /// <summary>
        /// Parse a dictionary of registry keys and assign the values to the corresponding class configuration settings
        /// </summary>
        /// <param name="registryValues">The registry key value pairs</param>
        public FacadeConfiguration(Dictionary<string, string> registryValues)
        {
            // set defaults first
            _isProduction = true; // default to true as it is more restrictive
            _sitesFileName = MdwsConstants.DEFAULT_SITES_FILE_NAME; //
            _version = "0.0.0";

            if (registryValues == null || registryValues.Count == 0)
            {
                return;
            }

            if (registryValues.ContainsKey(MdwsConfigConstants.FACADE_SITES_FILE))
            {
                _sitesFileName = registryValues[MdwsConfigConstants.FACADE_SITES_FILE];
            }

            if (registryValues.ContainsKey(MdwsConfigConstants.FACADE_PRODUCTION))
            {
                bool success = Boolean.TryParse(registryValues[MdwsConfigConstants.FACADE_PRODUCTION], out _isProduction);
            }

            if (registryValues.ContainsKey(MdwsConfigConstants.FACADE_VERSION))
            {
                _version = registryValues[MdwsConfigConstants.FACADE_VERSION];
            }

        }
    }
}
