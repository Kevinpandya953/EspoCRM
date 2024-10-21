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
    public partial class EditCasesCaseContact
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
        public string case_contact_id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            casesCaseContact = await EspoDbNewService.GetCasesCaseContactByCaseContactId(caseContactId:case_contact_id);
        }
        protected bool errorVisible;
        protected EspoNew.Server.Models.EspoDbNew.CasesCaseContact casesCaseContact;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.CasesCase> casesCasesForcaseId;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> contactscontactsForcontactId;


        protected int casesCasesForcaseIdCount;
        protected EspoNew.Server.Models.EspoDbNew.CasesCase casesCasesForcaseIdValue;
        protected async Task casesCasesForcaseIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetCases_cases(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                casesCasesForcaseId = result.Value.AsODataEnumerable();
                casesCasesForcaseIdCount = result.Count;

                if (!object.Equals(casesCaseContact.case_id, null))
                {
                    var valueResult = await EspoDbNewService.GetCases_cases(filter: $"case_id eq '{casesCaseContact.case_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        casesCasesForcaseIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load _case" });
            }
        }

        protected int contactscontactsForcontactIdCount;
        protected EspoNew.Server.Models.EspoDbNew.ContactsContact contactscontactsForcontactIdValue;
        protected async Task contactscontactsForcontactIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetContactscontacts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                contactscontactsForcontactId = result.Value.AsODataEnumerable();
                contactscontactsForcontactIdCount = result.Count;

                if (!object.Equals(casesCaseContact.contact_id, null))
                {
                    var valueResult = await EspoDbNewService.GetContactscontacts(filter: $"contact_id eq '{casesCaseContact.contact_id}'");
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
        protected async Task FormSubmit()
        {
            try
            {
                await EspoDbNewService.UpdateCasesCaseContact(caseContactId:case_contact_id, casesCaseContact);
                DialogService.Close(casesCaseContact);
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





        bool hascase_idValue;

        [Parameter]
        public string case_id { get; set; }

        bool hascontact_idValue;

        [Parameter]
        public string contact_id { get; set; }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            casesCaseContact = new EspoNew.Server.Models.EspoDbNew.CasesCaseContact();

            hascase_idValue = parameters.TryGetValue<string>("case_id", out var hascase_idResult);

            if (hascase_idValue)
            {
                casesCaseContact.case_id = hascase_idResult;
            }

            hascontact_idValue = parameters.TryGetValue<string>("contact_id", out var hascontact_idResult);

            if (hascontact_idValue)
            {
                casesCaseContact.contact_id = hascontact_idResult;
            }
            await base.SetParametersAsync(parameters);
        }
    }
}