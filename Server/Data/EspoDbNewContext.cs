using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EspoNew.Server.Models.EspoDbNew;

namespace EspoNew.Server.Data
{
    public partial class EspoDbNewContext : DbContext
    {
        public EspoDbNewContext()
        {
        }

        public EspoDbNewContext(DbContextOptions<EspoDbNewContext> options) : base(options)
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccount>()
              .HasOne(i => i.employee)
              .WithMany(i => i.Accountsaccounts)
              .HasForeignKey(i => i.assigned_employee_id)
              .HasPrincipalKey(i => i.employee_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccount>()
              .HasOne(i => i.campaign)
              .WithMany(i => i.Accountsaccounts)
              .HasForeignKey(i => i.campaign_id)
              .HasPrincipalKey(i => i.campaign_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact>()
              .HasOne(i => i.account)
              .WithMany(i => i.Accountsaccount_contacts)
              .HasForeignKey(i => i.account_id)
              .HasPrincipalKey(i => i.account_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact>()
              .HasOne(i => i.contact)
              .WithMany(i => i.Accountsaccount_contacts)
              .HasForeignKey(i => i.contact_id)
              .HasPrincipalKey(i => i.contact_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument>()
              .HasOne(i => i.account)
              .WithMany(i => i.Accountsaccount_documents)
              .HasForeignKey(i => i.account_id)
              .HasPrincipalKey(i => i.account_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument>()
              .HasOne(i => i.document)
              .WithMany(i => i.Accountsaccount_documents)
              .HasForeignKey(i => i.document_id)
              .HasPrincipalKey(i => i.document_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AddressAddressCity>()
              .HasOne(i => i.address_state)
              .WithMany(i => i.Addressaddress_cities)
              .HasForeignKey(i => i.state_id)
              .HasPrincipalKey(i => i.state_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AddressAddressState>()
              .HasOne(i => i.address_country)
              .WithMany(i => i.Addressaddress_states)
              .HasForeignKey(i => i.country_id)
              .HasPrincipalKey(i => i.country_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CallsCall>()
              .HasOne(i => i.account)
              .WithMany(i => i.Callscalls)
              .HasForeignKey(i => i.account_id)
              .HasPrincipalKey(i => i.account_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CallsCallContact>()
              .HasOne(i => i.call)
              .WithMany(i => i.Callscall_contacts)
              .HasForeignKey(i => i.call_id)
              .HasPrincipalKey(i => i.call_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CallsCallContact>()
              .HasOne(i => i.contact)
              .WithMany(i => i.Callscall_contacts)
              .HasForeignKey(i => i.contact_id)
              .HasPrincipalKey(i => i.contact_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CallsCallLead>()
              .HasOne(i => i.call)
              .WithMany(i => i.Callscall_leads)
              .HasForeignKey(i => i.call_id)
              .HasPrincipalKey(i => i.call_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CallsCallLead>()
              .HasOne(i => i.lead)
              .WithMany(i => i.Callscall_leads)
              .HasForeignKey(i => i.lead_id)
              .HasPrincipalKey(i => i.lead_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CasesCase>()
              .HasOne(i => i.account)
              .WithMany(i => i.Cases_cases)
              .HasForeignKey(i => i.account_id)
              .HasPrincipalKey(i => i.account_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CasesCase>()
              .HasOne(i => i.contact)
              .WithMany(i => i.Cases_cases)
              .HasForeignKey(i => i.contact_id)
              .HasPrincipalKey(i => i.contact_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CasesCase>()
              .HasOne(i => i.lead)
              .WithMany(i => i.Cases_cases)
              .HasForeignKey(i => i.lead_id)
              .HasPrincipalKey(i => i.lead_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CasesCaseContact>()
              .HasOne(i => i._case)
              .WithMany(i => i.Casescase_contacts)
              .HasForeignKey(i => i.case_id)
              .HasPrincipalKey(i => i.case_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CasesCaseContact>()
              .HasOne(i => i.contact)
              .WithMany(i => i.Casescase_contacts)
              .HasForeignKey(i => i.contact_id)
              .HasPrincipalKey(i => i.contact_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContact>()
              .HasOne(i => i.account)
              .WithMany(i => i.Contactscontacts)
              .HasForeignKey(i => i.account_id)
              .HasPrincipalKey(i => i.account_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContact>()
              .HasOne(i => i.address_city)
              .WithMany(i => i.Contactscontacts)
              .HasForeignKey(i => i.address_city_id)
              .HasPrincipalKey(i => i.city_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContact>()
              .HasOne(i => i.address_country)
              .WithMany(i => i.Contactscontacts)
              .HasForeignKey(i => i.address_country_id)
              .HasPrincipalKey(i => i.country_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContact>()
              .HasOne(i => i.address_state)
              .WithMany(i => i.Contactscontacts)
              .HasForeignKey(i => i.address_state_id)
              .HasPrincipalKey(i => i.state_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContact>()
              .HasOne(i => i.campaign)
              .WithMany(i => i.Contactscontacts)
              .HasForeignKey(i => i.campaign_id)
              .HasPrincipalKey(i => i.campaign_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument>()
              .HasOne(i => i.contact)
              .WithMany(i => i.Contactscontact_documents)
              .HasForeignKey(i => i.contact_id)
              .HasPrincipalKey(i => i.contact_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument>()
              .HasOne(i => i.document)
              .WithMany(i => i.Contactscontact_documents)
              .HasForeignKey(i => i.document_id)
              .HasPrincipalKey(i => i.document_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting>()
              .HasOne(i => i.contact)
              .WithMany(i => i.Contactscontact_meetings)
              .HasForeignKey(i => i.contact_id)
              .HasPrincipalKey(i => i.contact_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting>()
              .HasOne(i => i.meeting)
              .WithMany(i => i.Contactscontact_meetings)
              .HasForeignKey(i => i.meeting_id)
              .HasPrincipalKey(i => i.meeting_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity>()
              .HasOne(i => i.contact)
              .WithMany(i => i.Contactscontact_opportunities)
              .HasForeignKey(i => i.contact_id)
              .HasPrincipalKey(i => i.contact_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity>()
              .HasOne(i => i.opportunity)
              .WithMany(i => i.Contactscontact_opportunities)
              .HasForeignKey(i => i.opportunity_id)
              .HasPrincipalKey(i => i.opportunity_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.DocumentsDocument>()
              .HasOne(i => i.employee)
              .WithMany(i => i.Documentsdocuments)
              .HasForeignKey(i => i.assigned_employee_id)
              .HasPrincipalKey(i => i.employee_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead>()
              .HasOne(i => i.document)
              .WithMany(i => i.Documentsdocument_leads)
              .HasForeignKey(i => i.document_id)
              .HasPrincipalKey(i => i.document_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead>()
              .HasOne(i => i.lead)
              .WithMany(i => i.Documentsdocument_leads)
              .HasForeignKey(i => i.lead_id)
              .HasPrincipalKey(i => i.lead_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .HasOne(i => i.account)
              .WithMany(i => i.Emailemails)
              .HasForeignKey(i => i.account_id)
              .HasPrincipalKey(i => i.account_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>()
              .HasOne(i => i.contact)
              .WithMany(i => i.Employeesemployees)
              .HasForeignKey(i => i.contact_id)
              .HasPrincipalKey(i => i.contact_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .HasOne(i => i.campaign)
              .WithMany(i => i.Leadsleads)
              .HasForeignKey(i => i.campaign_id)
              .HasPrincipalKey(i => i.campaign_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>()
              .HasOne(i => i.account)
              .WithMany(i => i.Meetingsmeetings)
              .HasForeignKey(i => i.account_id)
              .HasPrincipalKey(i => i.account_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>()
              .HasOne(i => i.account)
              .WithMany(i => i.Opportunitiesopportunities)
              .HasForeignKey(i => i.account_id)
              .HasPrincipalKey(i => i.account_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>()
              .HasOne(i => i.employee)
              .WithMany(i => i.Opportunitiesopportunities)
              .HasForeignKey(i => i.assigned_employee_id)
              .HasPrincipalKey(i => i.employee_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>()
              .HasOne(i => i.campaign)
              .WithMany(i => i.Opportunitiesopportunities)
              .HasForeignKey(i => i.campaign_id)
              .HasPrincipalKey(i => i.campaign_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>()
              .HasOne(i => i.contact)
              .WithMany(i => i.Opportunitiesopportunities)
              .HasForeignKey(i => i.contact_id)
              .HasPrincipalKey(i => i.contact_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TasksTask>()
              .HasOne(i => i.account)
              .WithMany(i => i.Taskstasks)
              .HasForeignKey(i => i.account_id)
              .HasPrincipalKey(i => i.account_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TasksTask>()
              .HasOne(i => i.employee)
              .WithMany(i => i.Taskstasks)
              .HasForeignKey(i => i.assigned_employee_id)
              .HasPrincipalKey(i => i.employee_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TasksTask>()
              .HasOne(i => i.contact)
              .WithMany(i => i.Taskstasks)
              .HasForeignKey(i => i.contact_id)
              .HasPrincipalKey(i => i.contact_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TasksTask>()
              .HasOne(i => i.email)
              .WithMany(i => i.Taskstasks)
              .HasForeignKey(i => i.email_id)
              .HasPrincipalKey(i => i.email_id);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccount>()
              .Property(p => p.name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccount>()
              .Property(p => p.deleted)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccount>()
              .Property(p => p.website)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccount>()
              .Property(p => p.type)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccount>()
              .Property(p => p.industry)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccount>()
              .Property(p => p.sic_code)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccount>()
              .Property(p => p.billing_address_street)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccount>()
              .Property(p => p.billing_address_city)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccount>()
              .Property(p => p.billing_address_state)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccount>()
              .Property(p => p.billing_address_country)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccount>()
              .Property(p => p.billing_address_postal_code)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccount>()
              .Property(p => p.shipping_address_street)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccount>()
              .Property(p => p.shipping_address_city)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccount>()
              .Property(p => p.shipping_address_state)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccount>()
              .Property(p => p.shipping_address_country)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccount>()
              .Property(p => p.shipping_address_postal_code)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccount>()
              .Property(p => p.campaign_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccount>()
              .Property(p => p.assigned_employee_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact>()
              .Property(p => p.account_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact>()
              .Property(p => p.contact_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact>()
              .Property(p => p.role)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact>()
              .Property(p => p.is_inactive)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact>()
              .Property(p => p.deleted)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument>()
              .Property(p => p.account_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument>()
              .Property(p => p.document_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument>()
              .Property(p => p.deleted)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CallsCall>()
              .Property(p => p.name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CallsCall>()
              .Property(p => p.deleted)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CallsCall>()
              .Property(p => p.status)
              .HasDefaultValueSql(@"(N'Planned')");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CallsCall>()
              .Property(p => p.date_start)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CallsCall>()
              .Property(p => p.date_end)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CallsCall>()
              .Property(p => p.direction)
              .HasDefaultValueSql(@"(N'Outbound')");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CallsCall>()
              .Property(p => p.parent_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CallsCall>()
              .Property(p => p.parent_type)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CallsCall>()
              .Property(p => p.account_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CallsCall>()
              .Property(p => p.assigned_employee_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CallsCallContact>()
              .Property(p => p.call_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CallsCallContact>()
              .Property(p => p.contact_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CallsCallContact>()
              .Property(p => p.status)
              .HasDefaultValueSql(@"(N'None')");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CallsCallContact>()
              .Property(p => p.deleted)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CallsCallLead>()
              .Property(p => p.call_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CallsCallLead>()
              .Property(p => p.lead_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CallsCallLead>()
              .Property(p => p.status)
              .HasDefaultValueSql(@"(N'None')");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CallsCallLead>()
              .Property(p => p.deleted)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CampaignCampaign>()
              .Property(p => p.name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CampaignCampaign>()
              .Property(p => p.deleted)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CampaignCampaign>()
              .Property(p => p.status)
              .HasDefaultValueSql(@"(N'Planning')");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CampaignCampaign>()
              .Property(p => p.type)
              .HasDefaultValueSql(@"(N'Email')");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CampaignCampaign>()
              .Property(p => p.start_date)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CampaignCampaign>()
              .Property(p => p.end_date)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CampaignCampaign>()
              .Property(p => p.budget)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CampaignCampaign>()
              .Property(p => p.mail_merge_only_with_address)
              .HasDefaultValueSql(@"((1))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CampaignCampaign>()
              .Property(p => p.budget_currency)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CampaignCampaign>()
              .Property(p => p.assigned_employee_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CampaignCampaign>()
              .Property(p => p.contacts_template_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CampaignCampaign>()
              .Property(p => p.leads_template_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CampaignCampaign>()
              .Property(p => p.accounts_template_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CampaignCampaign>()
              .Property(p => p.users_template_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CasesCase>()
              .Property(p => p.name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CasesCase>()
              .Property(p => p.deleted)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CasesCase>()
              .Property(p => p.status)
              .HasDefaultValueSql(@"(N'New')");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CasesCase>()
              .Property(p => p.priority)
              .HasDefaultValueSql(@"(N'Normal')");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CasesCase>()
              .Property(p => p.type)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CasesCase>()
              .Property(p => p.account_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CasesCase>()
              .Property(p => p.lead_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CasesCase>()
              .Property(p => p.contact_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CasesCase>()
              .Property(p => p.inbound_email_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CasesCase>()
              .Property(p => p.assigned_employee_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CasesCaseContact>()
              .Property(p => p.case_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CasesCaseContact>()
              .Property(p => p.contact_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CasesCaseContact>()
              .Property(p => p.deleted)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContact>()
              .Property(p => p.deleted)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContact>()
              .Property(p => p.salutation_name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContact>()
              .Property(p => p.first_name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContact>()
              .Property(p => p.last_name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContact>()
              .Property(p => p.do_not_call)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContact>()
              .Property(p => p.address_street)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContact>()
              .Property(p => p.address_city_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContact>()
              .Property(p => p.address_state_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContact>()
              .Property(p => p.address_country_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContact>()
              .Property(p => p.address_postal_code)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContact>()
              .Property(p => p.middle_name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContact>()
              .Property(p => p.account_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContact>()
              .Property(p => p.campaign_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContact>()
              .Property(p => p.assigned_employee_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument>()
              .Property(p => p.contact_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument>()
              .Property(p => p.document_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument>()
              .Property(p => p.deleted)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting>()
              .Property(p => p.contact_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting>()
              .Property(p => p.meeting_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting>()
              .Property(p => p.status)
              .HasDefaultValueSql(@"(N'None')");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting>()
              .Property(p => p.deleted)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity>()
              .Property(p => p.contact_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity>()
              .Property(p => p.opportunity_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity>()
              .Property(p => p.role)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity>()
              .Property(p => p.deleted)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.DocumentsDocument>()
              .Property(p => p.name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.DocumentsDocument>()
              .Property(p => p.deleted)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.DocumentsDocument>()
              .Property(p => p.status)
              .HasDefaultValueSql(@"(N'Active')");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.DocumentsDocument>()
              .Property(p => p.type)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.DocumentsDocument>()
              .Property(p => p.publish_date)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.DocumentsDocument>()
              .Property(p => p.expiration_date)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.DocumentsDocument>()
              .Property(p => p.file_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.DocumentsDocument>()
              .Property(p => p.assigned_employee_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.DocumentsDocument>()
              .Property(p => p.folder_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead>()
              .Property(p => p.document_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead>()
              .Property(p => p.lead_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead>()
              .Property(p => p.deleted)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.deleted)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.from_string)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.reply_to_string)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.is_replied)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.message_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.message_id_internal)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.is_html)
              .HasDefaultValueSql(@"((1))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.status)
              .HasDefaultValueSql(@"(N'Archived')");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.has_attachment)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.date_sent)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.delivery_date)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.is_system)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.ics_event_uid)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.from_email_address_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.parent_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.parent_type)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.sent_by_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.assigned_employee_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.replied_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.created_event_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.created_event_type)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.group_folder_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.account_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.deleted)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.email_address)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.status)
              .HasDefaultValueSql(@"(N'Active')");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.host)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.port)
              .HasDefaultValueSql(@"((993))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.security)
              .HasDefaultValueSql(@"(N'SSL')");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.username)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.password)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.sent_folder)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.store_sent_emails)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.keep_fetched_emails_unread)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.fetch_since)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.connected_at)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.use_imap)
              .HasDefaultValueSql(@"((1))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.use_smtp)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.smtp_host)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.smtp_port)
              .HasDefaultValueSql(@"((587))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.smtp_auth)
              .HasDefaultValueSql(@"((1))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.smtp_security)
              .HasDefaultValueSql(@"(N'TLS')");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.smtp_username)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.smtp_password)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.smtp_auth_mechanism)
              .HasDefaultValueSql(@"(N'login')");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.imap_handler)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.smtp_handler)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.email_folder_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.assigned_employee_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>()
              .Property(p => p.deleted)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>()
              .Property(p => p.employee_name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>()
              .Property(p => p.type)
              .HasDefaultValueSql(@"(N'regular')");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>()
              .Property(p => p.password)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>()
              .Property(p => p.auth_method)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>()
              .Property(p => p.api_key)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>()
              .Property(p => p.salutation_name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>()
              .Property(p => p.first_name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>()
              .Property(p => p.last_name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>()
              .Property(p => p.is_active)
              .HasDefaultValueSql(@"((1))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>()
              .Property(p => p.title)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>()
              .Property(p => p.avatar_color)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>()
              .Property(p => p.gender)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>()
              .Property(p => p.delete_id)
              .HasDefaultValueSql(@"(N'0')");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>()
              .Property(p => p.middle_name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>()
              .Property(p => p.default_team_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>()
              .Property(p => p.contact_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>()
              .Property(p => p.avatar_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>()
              .Property(p => p.dashboard_template_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>()
              .Property(p => p.working_time_calendar_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>()
              .Property(p => p.layout_set_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.deleted)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.salutation_name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.first_name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.last_name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.title)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.status)
              .HasDefaultValueSql(@"(N'New')");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.source)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.industry)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.opportunity_amount)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.website)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.address_street)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.address_city)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.address_state)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.address_country)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.address_postal_code)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.do_not_call)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.converted_at)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.account_name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.middle_name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.opportunity_amount_currency)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.assigned_employee_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.campaign_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.created_account_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.created_contact_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.created_opportunity_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>()
              .Property(p => p.name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>()
              .Property(p => p.deleted)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>()
              .Property(p => p.status)
              .HasDefaultValueSql(@"(N'Planned')");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>()
              .Property(p => p.date_start)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>()
              .Property(p => p.date_end)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>()
              .Property(p => p.is_all_day)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>()
              .Property(p => p.date_start_date)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>()
              .Property(p => p.date_end_date)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>()
              .Property(p => p.parent_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>()
              .Property(p => p.parent_type)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>()
              .Property(p => p.account_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>()
              .Property(p => p.assigned_employee_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>()
              .Property(p => p.name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>()
              .Property(p => p.deleted)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>()
              .Property(p => p.amount)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>()
              .Property(p => p.stage)
              .HasDefaultValueSql(@"(N'Prospecting')");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>()
              .Property(p => p.last_stage)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>()
              .Property(p => p.probability)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>()
              .Property(p => p.lead_source)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>()
              .Property(p => p.close_date)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>()
              .Property(p => p.amount_currency)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>()
              .Property(p => p.account_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>()
              .Property(p => p.contact_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>()
              .Property(p => p.campaign_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>()
              .Property(p => p.assigned_employee_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TargetTarget>()
              .Property(p => p.deleted)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TargetTarget>()
              .Property(p => p.salutation_name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TargetTarget>()
              .Property(p => p.first_name)
              .HasDefaultValueSql(@"(N'')");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TargetTarget>()
              .Property(p => p.last_name)
              .HasDefaultValueSql(@"(N'')");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TargetTarget>()
              .Property(p => p.title)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TargetTarget>()
              .Property(p => p.account_name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TargetTarget>()
              .Property(p => p.website)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TargetTarget>()
              .Property(p => p.address_street)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TargetTarget>()
              .Property(p => p.address_city)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TargetTarget>()
              .Property(p => p.address_state)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TargetTarget>()
              .Property(p => p.address_country)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TargetTarget>()
              .Property(p => p.address_postal_code)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TargetTarget>()
              .Property(p => p.do_not_call)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TargetTarget>()
              .Property(p => p.middle_name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TargetTarget>()
              .Property(p => p.assigned_employee_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TargetTargetList>()
              .Property(p => p.name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TargetTargetList>()
              .Property(p => p.deleted)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TargetTargetList>()
              .Property(p => p.assigned_user_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TasksTask>()
              .Property(p => p.name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TasksTask>()
              .Property(p => p.deleted)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TasksTask>()
              .Property(p => p.status)
              .HasDefaultValueSql(@"(N'Not Started')");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TasksTask>()
              .Property(p => p.priority)
              .HasDefaultValueSql(@"(N'Normal')");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TasksTask>()
              .Property(p => p.date_start)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TasksTask>()
              .Property(p => p.date_end)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TasksTask>()
              .Property(p => p.date_start_date)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TasksTask>()
              .Property(p => p.date_end_date)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TasksTask>()
              .Property(p => p.date_completed)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TasksTask>()
              .Property(p => p.parent_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TasksTask>()
              .Property(p => p.parent_type)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TasksTask>()
              .Property(p => p.account_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TasksTask>()
              .Property(p => p.contact_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TasksTask>()
              .Property(p => p.assigned_employee_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TasksTask>()
              .Property(p => p.email_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TeamsTeam>()
              .Property(p => p.name)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TeamsTeam>()
              .Property(p => p.deleted)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TeamsTeam>()
              .Property(p => p.layout_set_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TeamsTeam>()
              .Property(p => p.working_time_calendar_id)
              .HasDefaultValueSql(@"(NULL)");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CasesCase>()
              .Property(p => p.number)
              .ValueGeneratedOnAddOrUpdate().UseIdentityColumn()
              .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CallsCall>()
              .Property(p => p.date_start)
              .HasColumnType("datetime2");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.CallsCall>()
              .Property(p => p.date_end)
              .HasColumnType("datetime2");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.date_sent)
              .HasColumnType("datetime2");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmail>()
              .Property(p => p.delivery_date)
              .HasColumnType("datetime2");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>()
              .Property(p => p.connected_at)
              .HasColumnType("datetime2");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.LeadsLead>()
              .Property(p => p.converted_at)
              .HasColumnType("datetime2");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>()
              .Property(p => p.date_start)
              .HasColumnType("datetime2");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>()
              .Property(p => p.date_end)
              .HasColumnType("datetime2");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TasksTask>()
              .Property(p => p.date_start)
              .HasColumnType("datetime2");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TasksTask>()
              .Property(p => p.date_end)
              .HasColumnType("datetime2");

            builder.Entity<EspoNew.Server.Models.EspoDbNew.TasksTask>()
              .Property(p => p.date_completed)
              .HasColumnType("datetime2");
            this.OnModelBuilding(builder);
        }

        public DbSet<EspoNew.Server.Models.EspoDbNew.AccountsAccount> Accountsaccounts { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact> Accountsaccount_contacts { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument> Accountsaccount_documents { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.AddressAddressCity> Addressaddress_cities { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry> Addressaddress_countries { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.AddressAddressState> Addressaddress_states { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.CallsCall> Callscalls { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.CallsCallContact> Callscall_contacts { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.CallsCallLead> Callscall_leads { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.CampaignCampaign> Campaigncampaigns { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.CasesCase> Cases_cases { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.CasesCaseContact> Casescase_contacts { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.ContactsContact> Contactscontacts { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument> Contactscontact_documents { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting> Contactscontact_meetings { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity> Contactscontact_opportunities { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> Documentsdocuments { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead> Documentsdocument_leads { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.EmailEmail> Emailemails { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount> Emailemail_accounts { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> Employeesemployees { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.LeadsLead> Leadsleads { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting> Meetingsmeetings { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> Opportunitiesopportunities { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.TargetTarget> Targettargets { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.TargetTargetList> Targettarget_lists { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.TasksTask> Taskstasks { get; set; }

        public DbSet<EspoNew.Server.Models.EspoDbNew.TeamsTeam> Teamsteams { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    }
}