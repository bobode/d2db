using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace D2DB.Extentions
{
    public abstract class WebViewPageExt : WebViewPage
    {

        public void PrepareForView() { }

        protected override void SetViewData(ViewDataDictionary viewData)
        {
            PrepareForView();
            base.SetViewData(viewData);
        }
    }
}