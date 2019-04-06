using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Options;
using NorthwindApp.UI.Infrastructure.Configuration;
using NorthwindApp.UI.Interfaces;
using NorthwindApp.UI.Models;

namespace NorthwindApp.UI.Infrastructure.Builders
{
    public class SiteMapBuilder : ISiteMapBuilder
    {
        private readonly Breadcrumb _siteMap;
        private readonly IMapper _mapper;

        public SiteMapBuilder(IOptions<Breadcrumb> siteMapOptions, IMapper mapper)
        {
            _siteMap = siteMapOptions.Value;
            _mapper = mapper;
        }

        public IEnumerable<BreadcrumbViewModel> Build(string controller, string action)
        {
            return GetBreadcrumbs(controller, action, _siteMap)?.Reverse<BreadcrumbViewModel>();
        }

        private List<BreadcrumbViewModel> GetBreadcrumbs(string controller, string action, Breadcrumb node)
        {
            if (node == null)
            {
                return null;
            }

            var currentNode = _mapper.Map<BreadcrumbViewModel>(node);

            if (IsCurrentPage(node, controller, action))
            {
                currentNode.IsCurrentPage = true;

                return new List<BreadcrumbViewModel> { currentNode };
            }

            if (node.Breadcrumbs != null)
            {
                foreach (var breadcrumb in node.Breadcrumbs)
                {
                    var nodes = GetBreadcrumbs(controller, action, breadcrumb);
                    if (nodes != null)
                    {
                        nodes.Add(currentNode);
                        return nodes;
                    }
                }
            }

            return null;
        }

        private bool IsCurrentPage(Breadcrumb breadcrumb, string controller, string action) =>
            string.Equals(controller, breadcrumb.Controller, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(action, breadcrumb.Action, StringComparison.OrdinalIgnoreCase);
    }
}
