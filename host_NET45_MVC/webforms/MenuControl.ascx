<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MenuControl.ascx.vb" Inherits="MenuControl" %>

<div  >
<!--Mega Menu Anchor-->
<a href="default.aspx" id="megaanchor" 
    style="color: #FFFFFF; font-size: small; ">Site Map</a>

<!--Mega Drop Down Menu HTML. Retain given CSS classes-->
<div id="megamenu1" class="megamenu">

<div class="column">
	<h3>Purchase Orders</h3>
	<ul>
<li><a href="pocreate.aspx" title="" >Raise Po</a></li>
<li><a href="invoicedefine.aspx" title="" >Invoice Define</a></li>
<li><a href="paymentrequest.aspx" title="" >Payment Req</a></li>
<li><a href="paylog.aspx" title="" >Pay</a></li>
<hr />
<li><a href="editpo.aspx" title="" >Edit Po</a></li>
<li><a href="editinvoice.aspx" title="" >Edit Invoice</a></li>
<li><a href="editpaymentreq.aspx" title="" >Edit Pay Request</a></li>
<li><a href="editpaylog.aspx" title="" >Edit Payments</a></li>
	</ul>
</div>
  <div class="column">
	<h3>Most Visited</h3>
	<ul>
<li><a href="Monitoring.aspx" title="" >Monitoring</a></li>
<li><a href="Pendinglist.aspx" title="" >Pending List</a></li>
<li><a href="PendingCollectedDocuments.aspx" title="" >Critical Items In Pending</a></li>
<li><a href="PackingListToday.aspx" title="" >Packing List For Today</a></li>
<li><a href="Payments.aspx" title="" >Payments</a></li>
<li><a href="open_po.aspx" title="" >Open Pos</a></li>
<li><a href="PaymentTerms.aspx" title="" >Payment Terms</a></li>
</ul>
</div>

<div class="column">
	<h3>Reports</h3>
	<ul>
<li><a href="FollowUpReport.aspx" title="" >FollowUp Report Exc. VAT</a></li>
<li><a href="FollowUpReportWithVAT.aspx" title="" >FollowUp Report With VAT</a></li>
<li><a href="FollowUpReportBySupplierExcVAT.aspx" title="" >FollowUp Report By Suppliers Exc. VAT</a></li>
<li><a href="FollowUpReportBySupplierWithVAT.aspx" title="" >FollowUp Report By Suppliers With VAT</a></li>
<li><a href="FollowUpReport2.aspx" title="" >FollowUp Report BACKUPs</a></li>
<li><a href="CostReport.aspx" title="" >Cost Report</a></li>
<li><a href="CostReportEdit.aspx" title="" >Edit Cost Report</a></li>
<li><a href="ComparePo.aspx" title="" >Compare Pos</a></li>
<li><a href="fixrates.aspx" title="" >Fix Rates</a></li>
	</ul>
</div>

<div class="column">
	<h3>Reports</h3>
	<ul>
<li><a href="PO_Tracker.aspx" title="" >PO Tracker</a></li>
<li><a href="PO_TrackerChart.aspx" title="" >PO Tracker Chart</a></li>
<li><a href="PO_TrackerSummary.aspx" title="" >PO Tracker Summary</a></li>
<li><a href="PO_TrackerSummaryChart.aspx" title="" >PO Tracker Summary Chart</a></li>
<li><a href="POtrack.aspx" title="" >PO Track</a></li>
<li><a href="POweek.aspx" title="" >PO Week</a></li>
<li><a href="POforeignCurrency.aspx" title="" >PO Foreign Currency</a></li>
<li><a href="ProjectBalanceBreakdown.aspx" title="" >Balance Breakdown</a></li>
<li><a href="salaryBreakdown.aspx" title="" >Salary Breakdown</a></li>
<li><a href="1StoPTS_summary.aspx" title="" >1S Costs On PTS</a></li>
	</ul>
</div>

<br style="clear: left" /> <!--Break after 3rd column. Move this if desired-->

<div class="column">
	<h3>Contracts</h3>
	<ul>
<li><a href="contractview.aspx" title="" >See Contracts</a></li>
<li><a href="contractenter.aspx" title="" >Contract Enter</a></li>
<li><a href="contractnotes.aspx" title="" >Contract Notes</a></li>
	</ul>
</div>

<div class="column">
	<h3>Contract PO Matching</h3>
	<ul>
<li><a href="ContractVersusPo.aspx" title="" >Contract Versus PO</a></li>
<li><a href="ContractVersusPo2.aspx" title="" >Contract Versus PO 2</a></li>
<li><a href="ContractAddendums.aspx" title="" >Contracts&Addendum versus PO</a></li>
	</ul>
</div>

<div class="column">
	<h3>Delivery Documents</h3>
	<ul>
<li><a href="nakladnaya.aspx" title="" >Delivery Documents</a></li>
<li><a href="missingdocs.aspx" title="" >Missing Delivery Docs</a></li>
<li><a href="PTS_1S_DeliveryDocComparison.aspx" title="" >PTS 1S Delivery Doc Comparison</a></li>
<li><a href="DeliveryReport.aspx" title="" >Delivery Doc versus Payments</a></li>
	</ul>
</div>

<div class="column">
	<h3>Search</h3>
	<ul>
<li><a href="SearchByPayReqNo.aspx" title="" >Search PayReqNo</a></li>
<li><a href="SearchInvoice.aspx" title="" >Search</a></li>
	</ul>
</div>

<br style="clear: left" /> <!--Break after 3rd column. Move this if desired-->

<div class="column">
	<h3>Misc.</h3>
	<ul>
<li><a href="projects.aspx" title="" >Projects</a></li>
<li><a href="EditSupplier.aspx" title="" >Edt Supplier</a></li>
<li><a href="Ageing.aspx" title="" >Ageing</a></li>
<li><a href="poTimeStamp.aspx" title="" >PO Time Stamp</a></li>
<li><a href="analytics.aspx" title="" >Analytics</a></li>
<li><a href="FollowUsers.aspx" title="" >Follow Users</a></li>
	</ul>
</div>

<div class="column">
	<h3>Procurement</h3>
	<ul>
<li><a href="DeliveryFollowUp.aspx" title="" >Delivery</a></li>
<li><a href="DeliveryTotalPerson.aspx" title="" >Delivery Total</a></li>
<li><a href="SupplierAdressBook.aspx" title="" >Suppliers Book</a></li>
<li><a href="TOP20Supplier.aspx" title="" >Top Suppliers</a></li>
	</ul>
</div>

<div class="column">
	<h3>Approvals</h3>
	<ul>
<li><a href="PendingApproval.aspx" title="" >Pending Approval</a></li>
<li><a href="PoApproval.aspx" title="" >PO Approval</a></li>
<li><a href="PoApprovalFrame.aspx" title="" >PO Approval Frame</a></li>
<li><a href="ApprovalMatrix.aspx" title="" >Approval Matrix</a></li>
<li><a href="PendinglistApprovedItems.aspx" title="" >Approved Items in Pending List</a></li>
<li><a href="PR_PMapproval.aspx" title="" >PM approvals</a></li>
	</ul>
</div>

<div class="column">
	<h3>Admin</h3>
	<ul>
<li><a href="ScheduledWorks_DataBaseBackUp.aspx" title="" >BackUp PTS</a></li>
<li><a href="z.aspx" title="" >User Accounts</a></li>
<li><a href="POTimeStamp.aspx" title="" >PO Time Stamp</a></li>
	</ul>
</div>

</div>

</div>

