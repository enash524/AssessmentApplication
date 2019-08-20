using System.Collections.Generic;
using System.Web.Optimization;

namespace AssessmentApplication.Optimization
{
	public class NonOrderingBundleOrderer : IBundleOrderer
	{
		#region Public Methods

		public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
		{
			return files;
		}

		#endregion Public Methods
	}
}