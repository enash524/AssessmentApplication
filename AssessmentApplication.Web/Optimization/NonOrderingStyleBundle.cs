using System;
using System.Web.Optimization;

namespace AssessmentApplication.Optimization
{
	public class NonOrderingStyleBundle : StyleBundle
	{
		#region Public Constructors

		public NonOrderingStyleBundle(string virtualPath)
			: base(virtualPath)
		{
		}

		public NonOrderingStyleBundle(string virtualPath, string cdnPath)
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

		#region Public Methods

		public override Bundle Include(params string[] virtualPaths)
		{
			foreach (var virtualPath in virtualPaths)
				base.Include(virtualPath, new CssRewriteUrlTransform());

			return this;
		}

		#endregion Public Methods
	}
}