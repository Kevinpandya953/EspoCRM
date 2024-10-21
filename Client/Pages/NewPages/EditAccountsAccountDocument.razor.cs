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
    public partial class EditAccountsAccountDocument
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

        [Parameter]
        public string account_document_id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            accountsAccountDocument = await EspoDbNewService.GetAccountsAccountDocumentByAccountDocumentId(accountDocumentId:account_document_id);
        }
        protected bool errorVisible;
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument accountsAccountDocument;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AccountsAccount> accountsaccountsForaccountId;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> documentsdocumentsFordocumentId;


        protected int accountsaccountsForaccountIdCount;
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsaccountsForaccountIdValue;
        protected async Task accountsaccountsForaccountIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAccountsaccounts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                accountsaccountsForaccountId = result.Value.AsODataEnumerable();
                accountsaccountsForaccountIdCount = result.Count;

                if (!object.Equals(accountsAccountDocument.account_id, null))
                {
                    var valueResult = await EspoDbNewService.GetAccountsaccounts(filter: $"account_id eq '{accountsAccountDocument.account_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        accountsaccountsForaccountIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load account" });
            }
        }

        protected int documentsdocumentsFordocumentIdCount;
        protected EspoNew.Server.Models.EspoDbNew.DocumentsDocument documentsdocumentsFordocumentIdValue;
        protected async Task documentsdocumentsFordocumentIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetDocumentsdocuments(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(folder_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                documentsdocumentsFordocumentId = result.Value.AsODataEnumerable();
                documentsdocumentsFordocumentIdCount = result.Count;

                if (!object.Equals(accountsAccountDocument.document_id, null))
                {
                    var valueResult = await EspoDbNewService.GetDocumentsdocuments(filter: $"document_id eq '{accountsAccountDocument.document_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        documentsdocumentsFordocumentIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load document" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                await EspoDbNewService.UpdateAccountsAccountDocument(accountDocumentId:account_document_id, accountsAccountDocument);
                DialogService.Close(accountsAccountDocument);
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }





        bool hasaccount_idValue;

        [Parameter]
        public string account_id { get; set; }

        bool hasdocument_idValue;

        [Parameter]
        public string document_id { get; set; }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            accountsAccountDocument = new EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument();

            hasaccount_idValue = parameters.TryGetValue<string>("account_id", out var hasaccount_idResult);

            if (hasaccount_idValue)
            {
                accountsAccountDocument.account_id = hasaccount_idResult;
            }

            hasdocument_idValue = parameters.TryGetValue<string>("document_id", out var hasdocument_idResult);

            if (hasdocument_idValue)
            {
                accountsAccountDocument.document_id = hasdocument_idResult;
            }
            await base.SetParametersAsync(parameters);
        }
    }
}