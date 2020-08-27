using System;
using System.Web.Optimization;

namespace AssessmentApplication.Optimization
{
    public class NonOrderingScriptBundle : ScriptBundle
    {
        #region Public Constructors

        public NonOrderingScriptBundle(string virtualPath)
            : base(virtualPath)
        {
        }

        public NonOrderingScriptBundle(string virtualPath, string cdnPath)
            : base(virtualPath, cdnPath)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override IBundleOrderer Orderer
        {
            get { return new NonOrderingBundleOrderer(); }
            set { throw new Exception("Unable to override Non-Ordered bundler"); }
        }

        #endregion Public Properties
    }
}
