
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ShopM4_DataMigrations.Data;
using ShopM4_Models;
using ShopM4_Models.ViewModels;
using ShopM4_Utility;
using ShopM4_DataMigrations.Repository.IRepository;
using ShopM4_Utility.BrainTree;
using Braintree;
using ShopM4_Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace ShopM4.Controllers
{
    public class OrderController:Controller
    {
        IRepositoryOrderHeader repositoryOrderHeader;
        IRepositoryOrderDetail repositoryOrderDetail;
        IBrainTreeBridge brainTreeBridge;

        public OrderController(IRepositoryOrderHeader repositoryOrderHeader,
            IRepositoryOrderDetail repositoryOrderDetail, IBrainTreeBridge brainTreeBridge)
        {
            this.brainTreeBridge = brainTreeBridge;
            this.repositoryOrderDetail = repositoryOrderDetail;
            this.repositoryOrderHeader = repositoryOrderHeader;
        }

        public IActionResult Index()
        {
            OrderViewModel viewModel = new OrderViewModel()
            {
                OrderHeaderlist = repositoryOrderHeader.GetAll(),
                StatusList = PathManager.StatusList.ToList().
                             Select(x => new SelectListItem { Text = x, Value = x })
            };

            return View(viewModel);
        }
    }
}
