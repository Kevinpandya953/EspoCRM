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
    public partial class AddContactsContactDocument
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

        protected override async Task OnInitializedAsync()
        {
        }
        protected bool errorVisible;
        protected EspoNew.Server.Models.EspoDbNew.ContactsContactDocument contactsContactDocument;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> contactscontactsForcontactId;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> documentsdocumentsFordocumentId;


        protected int contactscontactsForcontactIdCount;
        protected EspoNew.Server.Models.EspoDbNew.ContactsContact contactscontactsForcontactIdValue;
        protected async Task contactscontactsForcontactIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetContactscontacts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                contactscontactsForcontactId = result.Value.AsODataEnumerable();
                contactscontactsForcontactIdCount = result.Count;

                if (!object.Equals(contactsContactDocument.contact_id, null))
                {
                    var valueResult = await EspoDbNewService.GetContactscontacts(filter: $"contact_id eq '{contactsContactDocument.contact_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        contactscontactsForcontactIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load contact" });
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

                if (!object.Equals(contactsContactDocument.document_id, null))
                {
                    var valueResult = await EspoDbNewService.GetDocumentsdocuments(filter: $"document_id eq '{contactsContactDocument.document_id}'");
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
                await EspoDbNewService.CreateContactsContactDocument(contactsContactDocument);
                DialogService.Close(contactsContactDocument);
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





        bool hascontact_idValue;

        [Parameter]
        public string contact_id { get; set; }

        bool hasdocument_idValue;

        [Parameter]
        public string document_id { get; set; }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            contactsContactDocument = new EspoNew.Server.Models.EspoDbNew.ContactsContactDocument();

            hascontact_idValue = parameters.TryGetValue<string>("contact_id", out var hascontact_idResult);

            if (hascontact_idValue)
            {
                contactsContactDocument.contact_id = hascontact_idResult;
            }

            hasdocument_idValue = parameters.TryGetValue<string>("document_id", out var hasdocument_idResult);

            if (hasdocument_idValue)
            {
                contactsContactDocument.document_id = hasdocument_idResult;
            }
            await base.SetParametersAsync(parameters);
        }
    }
}