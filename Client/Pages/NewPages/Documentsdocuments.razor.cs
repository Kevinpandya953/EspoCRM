using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace EspoNew.Client.Pages.NewPages
{
    public partial class Documentsdocuments
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        public EspoDbNewService EspoDbNewService { get; set; }

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> documentsdocuments;

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> grid0;
        protected int count;

        protected string search = "";

        protected async Task Search(ChangeEventArgs args)
        {
            search = $"{args.Value}";

            await grid0.GoToPage(0);

            await grid0.Reload();
        }

        protected async Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetDocumentsdocuments(filter: $@"(contains(ETag,""{search}"") or contains(document_id,""{search}"") or contains(name,""{search}"") or contains(status,""{search}"") or contains(type,""{search}"") or contains(description,""{search}"") or contains(file_id,""{search}"") or contains(assigned_employee_id,""{search}"") or contains(folder_id,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", expand: "employee", orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null);
                documentsdocuments = result.Value.AsODataEnumerable();
                count = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Documentsdocuments" });
            }
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddDocumentsDocument>("Add DocumentsDocument", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> args)
        {
            await DialogService.OpenAsync<EditDocumentsDocument>("Edit DocumentsDocument", new Dictionary<string, object> { {"document_id", args.Data.document_id} });
            await grid0.Reload();
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.DocumentsDocument documentsDocument)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteDocumentsDocument(documentId:documentsDocument.document_id);

                    if (deleteResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete DocumentsDocument"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await EspoDbNewService.ExportDocumentsdocumentsToCSV(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "employee",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Documentsdocuments");
            }

            if (args == null || args.Value == "xlsx")
            {
                await EspoDbNewService.ExportDocumentsdocumentsToExcel(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "employee",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Documentsdocuments");
            }
        }

        protected EspoNew.Server.Models.EspoDbNew.DocumentsDocument documentsDocumentChild;
        protected async Task GetChildData(EspoNew.Server.Models.EspoDbNew.DocumentsDocument args)
        {
            documentsDocumentChild = args;
            var Accountsaccount_documentsResult = await EspoDbNewService.GetAccountsaccount_documents();
            if (Accountsaccount_documentsResult != null)
            {
                args.Accountsaccount_documents = Accountsaccount_documentsResult.Value.ToList();
            }
            var Contactscontact_documentsResult = await EspoDbNewService.GetContactscontact_documents();
            if (Contactscontact_documentsResult != null)
            {
                args.Contactscontact_documents = Contactscontact_documentsResult.Value.ToList();
            }
            var Documentsdocument_leadsResult = await EspoDbNewService.GetDocumentsdocument_leads();
            if (Documentsdocument_leadsResult != null)
            {
                args.Documentsdocument_leads = Documentsdocument_leadsResult.Value.ToList();
            }
        }
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument accountsAccountDocumentAccountsaccount_documents;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AccountsAccount> accountsaccountsForaccountIdAccountsaccount_documents;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> documentsdocumentsFordocumentIdAccountsaccount_documents;

        protected int accountsaccountsForaccountIdAccountsaccount_documentsCount;
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsaccountsForaccountIdAccountsaccount_documentsValue;
        protected async Task accountsaccountsForaccountIdAccountsaccount_documentsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAccountsaccounts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                accountsaccountsForaccountIdAccountsaccount_documents = result.Value.AsODataEnumerable();
                accountsaccountsForaccountIdAccountsaccount_documentsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load account" });
            }
        }

        protected int documentsdocumentsFordocumentIdAccountsaccount_documentsCount;
        protected EspoNew.Server.Models.EspoDbNew.DocumentsDocument documentsdocumentsFordocumentIdAccountsaccount_documentsValue;
        protected async Task documentsdocumentsFordocumentIdAccountsaccount_documentsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetDocumentsdocuments(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(folder_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                documentsdocumentsFordocumentIdAccountsaccount_documents = result.Value.AsODataEnumerable();
                documentsdocumentsFordocumentIdAccountsaccount_documentsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load document" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument> Accountsaccount_documentsDataGrid;

        protected async Task Accountsaccount_documentsAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.DocumentsDocument data)
        {

            var dialogResult = await DialogService.OpenAsync<AddAccountsAccountDocument>("Add Accountsaccount_documents", new Dictionary<string, object> {  });
            await GetChildData(data);
            await Accountsaccount_documentsDataGrid.Reload();

        }

        protected async Task Accountsaccount_documentsRowSelect(EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument args, EspoNew.Server.Models.EspoDbNew.DocumentsDocument data)
        {
            var dialogResult = await DialogService.OpenAsync<EditAccountsAccountDocument>("Edit Accountsaccount_documents", new Dictionary<string, object> { {"account_document_id", args.account_document_id} });
            await GetChildData(data);
            await Accountsaccount_documentsDataGrid.Reload();
        }

        protected async Task Accountsaccount_documentsDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument accountsAccountDocument)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteAccountsAccountDocument(accountDocumentId:accountsAccountDocument.account_document_id);

                    await GetChildData(documentsDocumentChild);

                    if (deleteResult != null)
                    {
                        await Accountsaccount_documentsDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete AccountsAccountDocument"
                });
            }
        }
        protected EspoNew.Server.Models.EspoDbNew.ContactsContactDocument contactsContactDocumentContactscontact_documents;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> contactscontactsForcontactIdContactscontact_documents;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> documentsdocumentsFordocumentIdContactscontact_documents;

        protected int contactscontactsForcontactIdContactscontact_documentsCount;
        protected EspoNew.Server.Models.EspoDbNew.ContactsContact contactscontactsForcontactIdContactscontact_documentsValue;
        protected async Task contactscontactsForcontactIdContactscontact_documentsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetContactscontacts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                contactscontactsForcontactIdContactscontact_documents = result.Value.AsODataEnumerable();
                contactscontactsForcontactIdContactscontact_documentsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load contact" });
            }
        }

        protected int documentsdocumentsFordocumentIdContactscontact_documentsCount;
        protected EspoNew.Server.Models.EspoDbNew.DocumentsDocument documentsdocumentsFordocumentIdContactscontact_documentsValue;
        protected async Task documentsdocumentsFordocumentIdContactscontact_documentsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetDocumentsdocuments(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(folder_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                documentsdocumentsFordocumentIdContactscontact_documents = result.Value.AsODataEnumerable();
                documentsdocumentsFordocumentIdContactscontact_documentsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load document" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument> Contactscontact_documentsDataGrid;

        protected async Task Contactscontact_documentsAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.DocumentsDocument data)
        {

            var dialogResult = await DialogService.OpenAsync<AddContactsContactDocument>("Add Contactscontact_documents", new Dictionary<string, object> {  });
            await GetChildData(data);
            await Contactscontact_documentsDataGrid.Reload();

        }

        protected async Task Contactscontact_documentsRowSelect(EspoNew.Server.Models.EspoDbNew.ContactsContactDocument args, EspoNew.Server.Models.EspoDbNew.DocumentsDocument data)
        {
            var dialogResult = await DialogService.OpenAsync<EditContactsContactDocument>("Edit Contactscontact_documents", new Dictionary<string, object> { {"contact_document_id", args.contact_document_id} });
            await GetChildData(data);
            await Contactscontact_documentsDataGrid.Reload();
        }

        protected async Task Contactscontact_documentsDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.ContactsContactDocument contactsContactDocument)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteContactsContactDocument(contactDocumentId:contactsContactDocument.contact_document_id);

                    await GetChildData(documentsDocumentChild);

                    if (deleteResult != null)
                    {
                        await Contactscontact_documentsDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete ContactsContactDocument"
                });
            }
        }
        protected EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead documentsDocumentLeadDocumentsdocument_leads;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> documentsdocumentsFordocumentIdDocumentsdocument_leads;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.LeadsLead> leadsleadsForleadIdDocumentsdocument_leads;

        protected int documentsdocumentsFordocumentIdDocumentsdocument_leadsCount;
        protected EspoNew.Server.Models.EspoDbNew.DocumentsDocument documentsdocumentsFordocumentIdDocumentsdocument_leadsValue;
        protected async Task documentsdocumentsFordocumentIdDocumentsdocument_leadsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetDocumentsdocuments(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(folder_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                documentsdocumentsFordocumentIdDocumentsdocument_leads = result.Value.AsODataEnumerable();
                documentsdocumentsFordocumentIdDocumentsdocument_leadsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load document" });
            }
        }

        protected int leadsleadsForleadIdDocumentsdocument_leadsCount;
        protected EspoNew.Server.Models.EspoDbNew.LeadsLead leadsleadsForleadIdDocumentsdocument_leadsValue;
        protected async Task leadsleadsForleadIdDocumentsdocument_leadsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetLeadsleads(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(created_opportunity_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                leadsleadsForleadIdDocumentsdocument_leads = result.Value.AsODataEnumerable();
                leadsleadsForleadIdDocumentsdocument_leadsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load lead" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead> Documentsdocument_leadsDataGrid;

        protected async Task Documentsdocument_leadsAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.DocumentsDocument data)
        {

            var dialogResult = await DialogService.OpenAsync<AddDocumentsDocumentLead>("Add Documentsdocument_leads", new Dictionary<string, object> {  });
            await GetChildData(data);
            await Documentsdocument_leadsDataGrid.Reload();

        }

        protected async Task Documentsdocument_leadsRowSelect(EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead args, EspoNew.Server.Models.EspoDbNew.DocumentsDocument data)
        {
            var dialogResult = await DialogService.OpenAsync<EditDocumentsDocumentLead>("Edit Documentsdocument_leads", new Dictionary<string, object> { {"document_lead_id", args.document_lead_id} });
            await GetChildData(data);
            await Documentsdocument_leadsDataGrid.Reload();
        }

        protected async Task Documentsdocument_leadsDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead documentsDocumentLead)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteDocumentsDocumentLead(documentLeadId:documentsDocumentLead.document_lead_id);

                    await GetChildData(documentsDocumentChild);

                    if (deleteResult != null)
                    {
                        await Documentsdocument_leadsDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete DocumentsDocumentLead"
                });
            }
        }
    }
}