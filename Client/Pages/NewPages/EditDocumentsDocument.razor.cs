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
    public partial class EditDocumentsDocument
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
        public string document_id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            documentsDocument = await EspoDbNewService.GetDocumentsDocumentByDocumentId(documentId:document_id);
        }
        protected bool errorVisible;
        protected EspoNew.Server.Models.EspoDbNew.DocumentsDocument documentsDocument;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> employeesemployeesForassignedEmployeeId;


        protected int employeesemployeesForassignedEmployeeIdCount;
        protected EspoNew.Server.Models.EspoDbNew.EmployeesEmployee employeesemployeesForassignedEmployeeIdValue;
        protected async Task employeesemployeesForassignedEmployeeIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetEmployeesemployees(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(layout_set_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                employeesemployeesForassignedEmployeeId = result.Value.AsODataEnumerable();
                employeesemployeesForassignedEmployeeIdCount = result.Count;

                if (!object.Equals(documentsDocument.assigned_employee_id, null))
                {
                    var valueResult = await EspoDbNewService.GetEmployeesemployees(filter: $"employee_id eq '{documentsDocument.assigned_employee_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        employeesemployeesForassignedEmployeeIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load employee" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                await EspoDbNewService.UpdateDocumentsDocument(documentId:document_id, documentsDocument);
                DialogService.Close(documentsDocument);
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





        bool hasassigned_employee_idValue;

        [Parameter]
        public string assigned_employee_id { get; set; }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            documentsDocument = new EspoNew.Server.Models.EspoDbNew.DocumentsDocument();

            hasassigned_employee_idValue = parameters.TryGetValue<string>("assigned_employee_id", out var hasassigned_employee_idResult);

            if (hasassigned_employee_idValue)
            {
                documentsDocument.assigned_employee_id = hasassigned_employee_idResult;
            }
            await base.SetParametersAsync(parameters);
        }
    }
}