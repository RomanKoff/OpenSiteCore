using Ans.Net6.Web.Nodes;
using Ans.Net6.Web.Services;

namespace OpenSiteCore.Controllers
{

	public class NodesController
		: _NodesController_Base
	{
		public NodesController(
			ILogger<_NodesController_Base> logger,
			ICurrentContext currentContext,
			IViewRenderService viewRender)
			: base(logger, currentContext, viewRender)
		{
		}
	}

}
