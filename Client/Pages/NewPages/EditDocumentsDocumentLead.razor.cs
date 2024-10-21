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
    public partial class EditDocumentsDocumentLead
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
        public string document_lead_id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            documentsDocumentLead = await EspoDbNewService.GetDocumentsDocumentLeadByDocumentLeadId(documentLeadId:document_lead_id);
        }
        protected bool errorVisible;
        protected EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead documentsDocumentLead;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> documentsdocumentsFordocumentId;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.LeadsLead> leadsleadsForleadId;


        protected int documentsdocumentsFordocumentIdCount;
        protected EspoNew.Server.Models.EspoDbNew.DocumentsDocument documentsdocumentsFordocumentIdValue;
        protected async Task documentsdocumentsFordocumentIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetDocumentsdocuments(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(folder_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                documentsdocumentsFordocumentId = result.Value.AsODataEnumerable();
                documentsdocumentsFordocumentIdCount = result.Count;

                if (!object.Equals(documentsDocumentLead.document_id, null))
                {
                    var valueResult = await EspoDbNewService.GetDocumentsdocuments(filter: $"document_id eq '{documentsDocumentLead.document_id}'");
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

        protected int leadsleadsForleadIdCount;
        protected EspoNew.Server.Models.EspoDbNew.LeadsLead leadsleadsForleadIdValue;
        protected async Task leadsleadsForleadIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetLeadsleads(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(created_opportunity_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                leadsleadsForleadId = result.Value.AsODataEnumerable();
                leadsleadsForleadIdCount = result.Count;

                if (!object.Equals(documentsDocumentLead.lead_id, null))
                {
                    var valueResult = await EspoDbNewService.GetLeadsleads(filter: $"lead_id eq '{documentsDocumentLead.lead_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        leadsleadsForleadIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load lead" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                await EspoDbNewService.UpdateDocumentsDocumentLead(documentLeadId:document_lead_id, documentsDocumentLead);
                DialogService.Close(documentsDocumentLead);
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





        bool hasdocument_idValue;

        [Parameter]
        public string document_id { get; set; }

        bool haslead_idValue;

        [Parameter]
        public string lead_id { get; set; }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            documentsDocumentLead = new EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead();

            hasdocument_idValue = parameters.TryGetValue<string>("document_id", out var hasdocument_idResult);

            if (hasdocument_idValue)
            {
                documentsDocumentLead.document_id = hasdocument_idResult;
            }

            haslead_idValue = parameters.TryGetValue<string>("lead_id", out var haslead_idResult);

            if (haslead_idValue)
            {
                documentsDocumentLead.lead_id = haslead_idResult;
            }
            await base.SetParametersAsync(parameters);
        }
    }
}