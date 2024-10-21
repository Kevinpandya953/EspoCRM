using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using EspoNew.Server.Data;

namespace EspoNew.Server.Controllers
{
    public partial class ExportEspoDbNewController : ExportController
    {
        private readonly EspoDbNewContext context;
        private readonly EspoDbNewService service;

        public ExportEspoDbNewController(EspoDbNewContext context, EspoDbNewService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/EspoDbNew/accountsaccounts/csv")]
        [HttpGet("/export/EspoDbNew/accountsaccounts/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAccountsaccountsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAccountsaccounts(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/accountsaccounts/excel")]
        [HttpGet("/export/EspoDbNew/accountsaccounts/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAccountsaccountsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAccountsaccounts(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/accountsaccount_contacts/csv")]
        [HttpGet("/export/EspoDbNew/accountsaccount_contacts/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAccountsaccount_contactsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAccountsaccount_contacts(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/accountsaccount_contacts/excel")]
        [HttpGet("/export/EspoDbNew/accountsaccount_contacts/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAccountsaccount_contactsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAccountsaccount_contacts(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/accountsaccount_documents/csv")]
        [HttpGet("/export/EspoDbNew/accountsaccount_documents/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAccountsaccount_documentsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAccountsaccount_documents(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/accountsaccount_documents/excel")]
        [HttpGet("/export/EspoDbNew/accountsaccount_documents/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAccountsaccount_documentsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAccountsaccount_documents(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/addressaddress_cities/csv")]
        [HttpGet("/export/EspoDbNew/addressaddress_cities/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAddressaddress_citiesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAddressaddress_cities(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/addressaddress_cities/excel")]
        [HttpGet("/export/EspoDbNew/addressaddress_cities/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAddressaddress_citiesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAddressaddress_cities(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/addressaddress_countries/csv")]
        [HttpGet("/export/EspoDbNew/addressaddress_countries/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAddressaddress_countriesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAddressaddress_countries(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/addressaddress_countries/excel")]
        [HttpGet("/export/EspoDbNew/addressaddress_countries/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAddressaddress_countriesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAddressaddress_countries(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/addressaddress_states/csv")]
        [HttpGet("/export/EspoDbNew/addressaddress_states/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAddressaddress_statesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAddressaddress_states(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/addressaddress_states/excel")]
        [HttpGet("/export/EspoDbNew/addressaddress_states/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAddressaddress_statesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAddressaddress_states(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/callscalls/csv")]
        [HttpGet("/export/EspoDbNew/callscalls/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCallscallsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCallscalls(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/callscalls/excel")]
        [HttpGet("/export/EspoDbNew/callscalls/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCallscallsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCallscalls(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/callscall_contacts/csv")]
        [HttpGet("/export/EspoDbNew/callscall_contacts/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCallscall_contactsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCallscall_contacts(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/callscall_contacts/excel")]
        [HttpGet("/export/EspoDbNew/callscall_contacts/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCallscall_contactsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCallscall_contacts(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/callscall_leads/csv")]
        [HttpGet("/export/EspoDbNew/callscall_leads/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCallscall_leadsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCallscall_leads(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/callscall_leads/excel")]
        [HttpGet("/export/EspoDbNew/callscall_leads/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCallscall_leadsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCallscall_leads(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/campaigncampaigns/csv")]
        [HttpGet("/export/EspoDbNew/campaigncampaigns/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCampaigncampaignsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCampaigncampaigns(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/campaigncampaigns/excel")]
        [HttpGet("/export/EspoDbNew/campaigncampaigns/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCampaigncampaignsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCampaigncampaigns(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/cases_cases/csv")]
        [HttpGet("/export/EspoDbNew/cases_cases/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCases_casesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCases_cases(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/cases_cases/excel")]
        [HttpGet("/export/EspoDbNew/cases_cases/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCases_casesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCases_cases(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/casescase_contacts/csv")]
        [HttpGet("/export/EspoDbNew/casescase_contacts/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCasescase_contactsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCasescase_contacts(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/casescase_contacts/excel")]
        [HttpGet("/export/EspoDbNew/casescase_contacts/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCasescase_contactsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCasescase_contacts(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/contactscontacts/csv")]
        [HttpGet("/export/EspoDbNew/contactscontacts/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportContactscontactsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetContactscontacts(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/contactscontacts/excel")]
        [HttpGet("/export/EspoDbNew/contactscontacts/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportContactscontactsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetContactscontacts(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/contactscontact_documents/csv")]
        [HttpGet("/export/EspoDbNew/contactscontact_documents/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportContactscontact_documentsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetContactscontact_documents(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/contactscontact_documents/excel")]
        [HttpGet("/export/EspoDbNew/contactscontact_documents/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportContactscontact_documentsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetContactscontact_documents(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/contactscontact_meetings/csv")]
        [HttpGet("/export/EspoDbNew/contactscontact_meetings/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportContactscontact_meetingsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetContactscontact_meetings(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/contactscontact_meetings/excel")]
        [HttpGet("/export/EspoDbNew/contactscontact_meetings/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportContactscontact_meetingsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetContactscontact_meetings(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/contactscontact_opportunities/csv")]
        [HttpGet("/export/EspoDbNew/contactscontact_opportunities/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportContactscontact_opportunitiesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetContactscontact_opportunities(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/contactscontact_opportunities/excel")]
        [HttpGet("/export/EspoDbNew/contactscontact_opportunities/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportContactscontact_opportunitiesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetContactscontact_opportunities(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/documentsdocuments/csv")]
        [HttpGet("/export/EspoDbNew/documentsdocuments/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDocumentsdocumentsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDocumentsdocuments(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/documentsdocuments/excel")]
        [HttpGet("/export/EspoDbNew/documentsdocuments/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDocumentsdocumentsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDocumentsdocuments(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/documentsdocument_leads/csv")]
        [HttpGet("/export/EspoDbNew/documentsdocument_leads/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDocumentsdocument_leadsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDocumentsdocument_leads(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/documentsdocument_leads/excel")]
        [HttpGet("/export/EspoDbNew/documentsdocument_leads/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDocumentsdocument_leadsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDocumentsdocument_leads(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/emailemails/csv")]
        [HttpGet("/export/EspoDbNew/emailemails/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEmailemailsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetEmailemails(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/emailemails/excel")]
        [HttpGet("/export/EspoDbNew/emailemails/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEmailemailsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetEmailemails(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/emailemail_accounts/csv")]
        [HttpGet("/export/EspoDbNew/emailemail_accounts/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEmailemail_accountsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetEmailemail_accounts(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/emailemail_accounts/excel")]
        [HttpGet("/export/EspoDbNew/emailemail_accounts/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEmailemail_accountsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetEmailemail_accounts(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/employeesemployees/csv")]
        [HttpGet("/export/EspoDbNew/employeesemployees/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEmployeesemployeesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetEmployeesemployees(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/employeesemployees/excel")]
        [HttpGet("/export/EspoDbNew/employeesemployees/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEmployeesemployeesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetEmployeesemployees(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/leadsleads/csv")]
        [HttpGet("/export/EspoDbNew/leadsleads/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLeadsleadsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetLeadsleads(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/leadsleads/excel")]
        [HttpGet("/export/EspoDbNew/leadsleads/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLeadsleadsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetLeadsleads(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/meetingsmeetings/csv")]
        [HttpGet("/export/EspoDbNew/meetingsmeetings/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMeetingsmeetingsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetMeetingsmeetings(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/meetingsmeetings/excel")]
        [HttpGet("/export/EspoDbNew/meetingsmeetings/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMeetingsmeetingsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetMeetingsmeetings(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/opportunitiesopportunities/csv")]
        [HttpGet("/export/EspoDbNew/opportunitiesopportunities/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportOpportunitiesopportunitiesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetOpportunitiesopportunities(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/opportunitiesopportunities/excel")]
        [HttpGet("/export/EspoDbNew/opportunitiesopportunities/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportOpportunitiesopportunitiesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetOpportunitiesopportunities(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/targettargets/csv")]
        [HttpGet("/export/EspoDbNew/targettargets/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTargettargetsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTargettargets(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/targettargets/excel")]
        [HttpGet("/export/EspoDbNew/targettargets/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTargettargetsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTargettargets(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/targettarget_lists/csv")]
        [HttpGet("/export/EspoDbNew/targettarget_lists/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTargettarget_listsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTargettarget_lists(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/targettarget_lists/excel")]
        [HttpGet("/export/EspoDbNew/targettarget_lists/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTargettarget_listsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTargettarget_lists(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/taskstasks/csv")]
        [HttpGet("/export/EspoDbNew/taskstasks/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTaskstasksToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTaskstasks(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/taskstasks/excel")]
        [HttpGet("/export/EspoDbNew/taskstasks/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTaskstasksToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTaskstasks(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/teamsteams/csv")]
        [HttpGet("/export/EspoDbNew/teamsteams/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTeamsteamsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTeamsteams(), Request.Query, false), fileName);
        }

        [HttpGet("/export/EspoDbNew/teamsteams/excel")]
        [HttpGet("/export/EspoDbNew/teamsteams/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTeamsteamsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTeamsteams(), Request.Query, false), fileName);
        }
    }
}
