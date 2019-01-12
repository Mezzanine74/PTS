using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace host_NET45_MVC.Areas.PtsWorker.Controllers
{
    public class EmailGeneratorController : _BaseController
    {
        private PTS_App_Code_VB_Project.PTS_MERCURY.db.SQL2008_794282_mercuryEntities db = new PTS_App_Code_VB_Project.PTS_MERCURY.db.SQL2008_794282_mercuryEntities();

        #region ViewModels

            public class ContractDetailsViewModel
            {
                public PTS_App_Code_VB_Project.PTS_MERCURY.db.View_ContractDetails ContractDetails { get; set; }
                public IEnumerable<PTS_App_Code_VB_Project.PTS_MERCURY.db.ApprovalStatusContractEF_Result> ApprovalStatusContractEFs { get; set; }
            }

            public class AddendumDetailsViewModel
            {
                public PTS_App_Code_VB_Project.PTS_MERCURY.db.View_AddendumDetails AddendumDetails { get; set; }
                public IEnumerable<PTS_App_Code_VB_Project.PTS_MERCURY.db.ApprovalStatusAddendumEF_Result> ApprovalStatusAddendumEFs { get; set; }
            }

            public class PoDetailsViewModel
            {
                public string PoNo { get; set; }
                public string Supplier { get; set; }
                public string Description { get; set; }
                public decimal PoTotalWithVAT { get; set; }
                public string Currency { get; set; }
                public decimal VAT { get; set; }
                public string CostCode { get; set; }
            }

            public class ContractAndPoDetailsViewModel
            {
                public ContractDetailsViewModel ContractDetails { get; set; }
                public PoDetailsViewModel PoDetails { get; set; }
            }
        
            public class AddendumAndPoDetailsViewModel
            {
                public AddendumDetailsViewModel AddendumDetails { get; set; }
                public PoDetailsViewModel PoDetails { get; set; }
            }
        
            public class RejectionCommentsContractViewModel
            {
                public string Comment { get; set; }
                public DateTime? When { get; set; }
            }

            public class RejectionCommentsAddendumViewModel
            {
                public string Comment { get; set; }
                public DateTime? When { get; set; }
            }

            public ContractDetailsViewModel getContractDetails(int id)
            {
                var approvalStatusContractEF = db.ApprovalStatusContractEF(id);
                List<PTS_App_Code_VB_Project.PTS_MERCURY.db.ApprovalStatusContractEF_Result> approvalStatusContractEFs =
                    new List<PTS_App_Code_VB_Project.PTS_MERCURY.db.ApprovalStatusContractEF_Result>();

                foreach (var item in approvalStatusContractEF)
                {
                    approvalStatusContractEFs.Add(item);
                }

                ContractDetailsViewModel contractDetailsViewModel =
                    new ContractDetailsViewModel
                    {
                        ContractDetails = db.View_ContractDetails.Where(c => c.ContractID == id).ToList()[0],
                        ApprovalStatusContractEFs = approvalStatusContractEFs
                    };

                return (contractDetailsViewModel);
            }

            public AddendumDetailsViewModel getAddendumDetails(int id)
            {
                var approvalStatusAddendumEF = db.ApprovalStatusAddendumEF(id);
                List<PTS_App_Code_VB_Project.PTS_MERCURY.db.ApprovalStatusAddendumEF_Result> approvalStatusAddendumEFs =
                    new List<PTS_App_Code_VB_Project.PTS_MERCURY.db.ApprovalStatusAddendumEF_Result>();

                foreach (var item in approvalStatusAddendumEF)
                {
                    approvalStatusAddendumEFs.Add(item);
                }

                AddendumDetailsViewModel addendumDetailsViewModel =
                    new AddendumDetailsViewModel
                    {
                        AddendumDetails = db.View_AddendumDetails.Where(c => c.AddendumID == id).ToList()[0],
                        ApprovalStatusAddendumEFs = approvalStatusAddendumEFs
                    };

                return addendumDetailsViewModel;
            }

            public PoDetailsViewModel getPoDetails(string id)
            {
                return (
                    db.Table2_PONo.Where(c => c.PO_No == id).Select(c => new PoDetailsViewModel
                    {
                        PoNo = c.PO_No,
                        Supplier = c.Table6_Supplier.SupplierName,
                        Description = c.Description,
                        PoTotalWithVAT = c.TotalPrice,
                        Currency = c.PO_Currency,
                        VAT = c.VATpercent,
                        CostCode = c.Table7_CostCode.CostCode.Trim() + " - " + c.Table7_CostCode.CodeDescription.Trim()
                    }).ToList()[0]
                );
            }

            public ContractAndPoDetailsViewModel getContractAndPoDetailsViewModel(string id)
            {
                int contractId = 0;
                try
                {
                    contractId = db.Table_Contracts.Where(c => c.PO_No == id).ToList()[0].ContractID;
                }
                catch (Exception)
                {
                 
                }
                return new ContractAndPoDetailsViewModel { ContractDetails = getContractDetails(contractId), PoDetails = getPoDetails(id) };
            }

            public AddendumAndPoDetailsViewModel getAddendumAndPoDetailsViewModel(string id)
            {
                int AddendumId = 0;
                try
                {
                    AddendumId = db.Table_Addendums.Where(c => c.PO_No == id).ToList()[0].AddendumID;
                }
                catch (Exception)
                {

                }
                return new AddendumAndPoDetailsViewModel { AddendumDetails = getAddendumDetails(AddendumId), PoDetails = getPoDetails(id) };
            }
        
            public List<RejectionCommentsContractViewModel> getRejectionCommentsContractViewModel(int id)
            {
                return db.Table_Contracts_UserRemarks.Where(c => c.ContractID == id && c.UserName == "lawyers")
                    .Select(c => new RejectionCommentsContractViewModel { Comment = c.Remark, When = c.LastUpdate }).ToList();
            }

            public List<RejectionCommentsAddendumViewModel> getRejectionCommentsAddendumViewModel(int id)
            {
                return db.Table_Addendum_UserRemarks.Where(c => c.AddendumID == id && c.UserName == "lawyers")
                    .Select(c => new RejectionCommentsAddendumViewModel { Comment = c.Remark, When = c.LastUpdate }).ToList();
            }

        #endregion

        #region Views
        
            public ActionResult EverybodyApprovedContract(int id) { return View(getContractDetails(id)); }

            public ActionResult EverybodyApprovedAddendum(int id) { return View(getAddendumDetails(id)); }

            public ActionResult LawyersApprovedContract(int id) { return View(getContractDetails(id)); }

            public ActionResult LawyersApprovedAddendum(int id) { return View(getAddendumDetails(id)); }

            public ActionResult ContractApprovalRequest(int id) { return View(getContractDetails(id)); }

            public ActionResult AddendumApprovalRequest(int id) { return View(getAddendumDetails(id)); }

            public ActionResult POHasBeenRaised(string id) { return View(getContractAndPoDetailsViewModel(id)); }

            public ActionResult POHasBeenRaisedForAddendum(string id) { return View(getAddendumAndPoDetailsViewModel(id)); }

            public ActionResult POHasBeenUpdated(string id) { return View(getContractAndPoDetailsViewModel(id)); }
        
            public ActionResult FrameContractApproved(int id) { return View(getContractDetails(id)); }
        
            public ActionResult DisagreementOnContract(int id) { return View(getContractDetails(id)); }

            public ActionResult DisagreementOnContractRemoved(int id) { return View(getContractDetails(id)); }
        
            public ActionResult DisagreementOnAddendum(int id) { return View(getAddendumDetails(id)); }

            public ActionResult DisagreementOnAddendumRemoved(int id) { return View(getAddendumDetails(id)); }
        
            public ActionResult ZeroValueAddendum(int id) { return View(getAddendumDetails(id)); }

            [ChildActionOnly]
            public ActionResult RejectionCommentsContract(int id)
            {
                return PartialView(getRejectionCommentsContractViewModel(id));
            }

            [ChildActionOnly]
            public ActionResult RejectionCommentsAddendum(int id)
            {
                return PartialView(getRejectionCommentsAddendumViewModel(id));
            }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}