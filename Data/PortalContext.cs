using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hackathon.SpotBot.Data
{
    public partial class PortalContext : DbContext
    {
        public PortalContext()
        {
        }

        public PortalContext(DbContextOptions<PortalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Advertiser> Advertisers { get; set; }
        public virtual DbSet<Agency> Agencies { get; set; }
        public virtual DbSet<Division> Divisions { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Market> Markets { get; set; }
        public virtual DbSet<PaymentInstrument> PaymentInstruments { get; set; }
        public virtual DbSet<PaymentRequest> PaymentRequests { get; set; }
        public virtual DbSet<PaymentRequestCustomer> PaymentRequestCustomers { get; set; }
        public virtual DbSet<PaymentRequestInvoice> PaymentRequestInvoices { get; set; }
        public virtual DbSet<PaymentResponse> PaymentResponses { get; set; }
        public virtual DbSet<PaymentResponseDivision> PaymentResponseDivisions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Advertiser>(entity =>
            {
                entity.HasIndex(e => new { e.AdvertiserCode, e.MarketID })
                    .HasName("IX_Advertiser_MarketID");

                entity.HasIndex(e => new { e.AdvertiserName, e.AdvertiserID })
                    .HasName("IX_Advertiser_AdvertiserID");

                entity.HasIndex(e => new { e.AdvertiserID, e.MarketID, e.AdvertiserCode })
                    .HasName("IX_Advertiser_MarketID_AdvertiserCode");

                entity.HasIndex(e => new { e.AdvertiserID, e.MarketID, e.AdvertiserMasterID })
                    .HasName("IX_Advertiser_MarketId_AdvertiserMasterID");

                entity.Property(e => e.AdvertiserCode).IsUnicode(false);

                entity.Property(e => e.AdvertiserName).IsUnicode(false);

                entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreditStatus).IsUnicode(false);
            });

            modelBuilder.Entity<Agency>(entity =>
            {
                entity.HasIndex(e => new { e.AgencyCode, e.MarketID })
                    .HasName("IX_Agency_MarketID");

                entity.HasIndex(e => new { e.AgencyName, e.AgencyCode, e.AgencyID })
                    .HasName("IX_Agency_AgencyID_WithName");

                entity.Property(e => e.AgencyCode).IsUnicode(false);

                entity.Property(e => e.AgencyName).IsUnicode(false);

                entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Division>(entity =>
            {
                entity.Property(e => e.CreatedByUserName).IsUnicode(false);

                entity.Property(e => e.DivisionName).IsUnicode(false);

                entity.Property(e => e.PaymentProcessorAccessKey).IsUnicode(false);

                entity.Property(e => e.PaymentProcessorProfileIdentifier).IsUnicode(false);

                entity.Property(e => e.PaymentProcessorSecretKey).IsUnicode(false);

                entity.Property(e => e.ShipFromStateAbbreviation).IsUnicode(false);

                entity.Property(e => e.ShipFromZipCode).IsUnicode(false);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.FileID)
                    .HasName("PK_Invoices");

                entity.HasIndex(e => new { e.FileID, e.InvoiceNumber })
                    .HasName("Index_InvoiceNumber");

                entity.HasIndex(e => new { e.AdvertiserID, e.FileProcessed, e.InActive })
                    .HasName("_dta_index_Invoice_8_66099276__K4_K14_K9");

                entity.HasIndex(e => new { e.InActive, e.MarketID, e.AdvertiserID })
                    .HasName("_dta_index_Invoice_117_66099276__K9_K3_K4");

                entity.HasIndex(e => new { e.MarketID, e.FileProcessed, e.InActive })
                    .HasName("IX_Invoice_MarkedId_FileProcessed_inactive");

                entity.HasIndex(e => new { e.AdvertiserID, e.InActive, e.MarketID, e.BroadcastMonth })
                    .HasName("_dta_index_Invoice_117_66099276__K3_K5_4_9");

                entity.HasIndex(e => new { e.FileProcessed, e.AgencyID, e.InActive, e.BroadcastMonth })
                    .HasName("_dta_index_Invoice_8_66099276__K14_K15_K9_K5");

                entity.HasIndex(e => new { e.FileProcessed, e.MarketID, e.AdvertiserID, e.InActive, e.FileID, e.BroadcastMonth })
                    .HasName("_dta_index_Invoice_8_66099276__K14_K3_K4_K9_K1_K5");

                entity.HasIndex(e => new { e.MarketID, e.AdvertiserID, e.BroadcastMonth, e.InActive, e.FilePath, e.InvoiceID, e.New, e.Viewed })
                    .HasName("_dta_index_Invoice_117_66099276__K2_K6_K7_3_4_5_9");

                entity.HasIndex(e => new { e.FileID, e.New, e.Viewed, e.CreationDate, e.UpdateDate, e.PdfFilePath, e.XlsFilePath, e.Ae, e.InActive, e.FileProcessed, e.BroadcastMonth, e.AdvertiserID, e.MarketID, e.AgencyID, e.InvoiceID })
                    .HasName("_dta_index_Invoice_8_66099276__K9_K14_K5_K4_K3_K15_K2_1_6_7_10_11_17_18_19");

                entity.HasIndex(e => new { e.FileID, e.Viewed, e.CreationDate, e.UpdateDate, e.PdfFilePath, e.XlsFilePath, e.Ae, e.New, e.FileProcessed, e.InActive, e.AdvertiserID, e.MarketID, e.AgencyID, e.InvoiceID, e.BroadcastMonth })
                    .HasName("_dta_index_Invoice_8_66099276__K6_K14_K9_K4_K3_K15_K2_K5_1_7_10_11_17_18_19");

                entity.Property(e => e.Ae).IsUnicode(false);

                entity.Property(e => e.BroadcastMonth).IsUnicode(false);

                entity.Property(e => e.CalculatedCurrentBalanceAmount).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FilePath).IsUnicode(false);

                entity.Property(e => e.FileProcessed).HasDefaultValueSql("((0))");

                entity.Property(e => e.InvoiceIdentifier).IsUnicode(false);

                entity.Property(e => e.InvoiceNumber).IsUnicode(false);

                entity.Property(e => e.InvoiceNumberWithoutPrefix).IsUnicode(false);

                entity.Property(e => e.IsPendingPaymentExist).HasDefaultValueSql("((0))");

                entity.Property(e => e.PdfFilePath).IsUnicode(false);

                entity.Property(e => e.PendingRefundAmount).HasDefaultValueSql("((0))");

                entity.Property(e => e.PendingScheduledPaymentAmount).HasDefaultValueSql("((0))");

                entity.Property(e => e.XlsFilePath).IsUnicode(false);

                entity.Property(e => e.Xmlfile).IsUnicode(false);
            });

            modelBuilder.Entity<Market>(entity =>
            {
                entity.Property(e => e.MarketID).ValueGeneratedNever();

                entity.Property(e => e.AppendInvoicePrefixOnPaymentScreens).HasDefaultValueSql("((1))");

                entity.Property(e => e.InvoiceMappingPrefix).IsUnicode(false);

                entity.Property(e => e.MarketName).IsUnicode(false);

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.Markets)
                    .HasForeignKey(d => d.DivisionID)
                    .HasConstraintName("FK_Market_Division");
            });

            modelBuilder.Entity<PaymentInstrument>(entity =>
            {
                entity.HasIndex(e => e.OriginalPaymentResponseID);

                entity.Property(e => e.AccountNumberLast4).IsUnicode(false);

                entity.Property(e => e.AccountType).IsUnicode(false);

                entity.Property(e => e.BillToCountry).IsUnicode(false);

                entity.Property(e => e.BillToPostalCode).IsUnicode(false);

                entity.Property(e => e.BillToState).IsUnicode(false);

                entity.Property(e => e.CreditCardLast4).IsUnicode(false);

                entity.Property(e => e.CreditCardName).IsUnicode(false);

                entity.Property(e => e.InstrumentName).IsUnicode(false);

                entity.Property(e => e.InstrumentType).IsUnicode(false);

                entity.Property(e => e.IsExpired).HasDefaultValueSql("((0))");

                entity.Property(e => e.PaymentToken).IsUnicode(false);

                entity.Property(e => e.RequestCheckNumber).IsUnicode(false);

                entity.Property(e => e.RoutingNumberLast4).IsUnicode(false);

                entity.HasOne(d => d.OriginalPaymentRequest)
                    .WithMany(p => p.PaymentInstruments)
                    .HasForeignKey(d => d.OriginalPaymentRequestID)
                    .HasConstraintName("FK_PaymentInstrument_PaymentRequest");

                entity.HasOne(d => d.OriginalPaymentResponse)
                    .WithMany(p => p.PaymentInstruments)
                    .HasForeignKey(d => d.OriginalPaymentResponseID)
                    .HasConstraintName("FK_PaymentInstrument_PaymentResponse");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PaymentInstruments)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PaymentInstrument_Users");
            });

            modelBuilder.Entity<PaymentRequest>(entity =>
            {
                entity.HasIndex(e => new { e.PaymentRequestID, e.PaymentProcessorTransactionStatusID })
                    .HasName("IX_PaymentRequest_PaymentProcessorTransactionStatusId2");

                entity.HasIndex(e => new { e.PaymentRequestID, e.PaymentProcessorTransactionStatusID, e.PaymentRequestDateTimeUtc })
                    .HasName("IX_PaymentRequest_Status_DateTime");

                entity.HasIndex(e => new { e.PaymentRequestID, e.UserID, e.PaymentInstrumentID, e.PaymentProcessorTransactionStatusID })
                    .HasName("IX_PaymentRequest_PaymentProcessorTransactionStatusId");

                entity.HasIndex(e => new { e.PaymentProcessorTransactionStatusID, e.PaymentRequestID, e.PaymentRequestDateTimeUtc, e.PaymentInstrumentID, e.UserID, e.PaymentExportStatusTypeID, e.RefundExportStatusTypeID, e.RefundExportDateTimeUtc })
                    .HasName("IX_PaymentRequest_ABunchOfStuff");

                entity.Property(e => e.MerchantIdentifier).IsUnicode(false);

                entity.Property(e => e.PaymentNotes).IsUnicode(false);

                entity.Property(e => e.PaymentProxyUserName).IsUnicode(false);

                entity.Property(e => e.PaymentRequestDateTimeUtc).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.PaymentUserName).IsUnicode(false);

                entity.Property(e => e.TransactionTypeCode).IsUnicode(false);

                entity.HasOne(d => d.Market)
                    .WithMany(p => p.PaymentRequests)
                    .HasForeignKey(d => d.MarketID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PaymentRequest_Market");

                entity.HasOne(d => d.PaymentInstrument)
                    .WithMany(p => p.PaymentRequests)
                    .HasForeignKey(d => d.PaymentInstrumentID)
                    .HasConstraintName("FK_PaymentRequest_PaymentInstrument");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PaymentRequests)
                    .HasForeignKey(d => d.UserID)
                    .HasConstraintName("FK_PaymentRequest_Users");
            });

            modelBuilder.Entity<PaymentRequestCustomer>(entity =>
            {
                entity.HasIndex(e => e.AdvertiserID);

                entity.HasIndex(e => e.AgencyID);

                entity.HasIndex(e => e.PaymentRequestID);

                entity.HasIndex(e => new { e.PaymentRequestCustomerID, e.PaymentRequestID })
                    .HasName("UNQ_PaymentRequestCustomer_PaymentRequestCustomerID_PaymentRequestID")
                    .IsUnique();

                entity.HasIndex(e => new { e.MarketID, e.PaymentRequestID, e.PaymentRequestCustomerID })
                    .HasName("IX_PaymentRequestCustomer_MarketId_PaymentRequestId_CustomerId");

                entity.HasIndex(e => new { e.PaymentRequestID, e.DivisionID, e.PaymentRequestCustomerID })
                    .HasName("IX_PaymentRequestCustomer_PaymentRequest_Division");

                entity.HasIndex(e => new { e.PaymentRequestCustomerID, e.PaymentRequestID, e.AdvertiserID, e.AgencyID, e.MarketID })
                    .HasName("IDX_PaymentRequestCustomer_MarketId");

                entity.Property(e => e.AgencyName).IsUnicode(false);

                entity.Property(e => e.AgencyNumber).IsUnicode(false);

                entity.Property(e => e.CustomerName).IsUnicode(false);

                entity.Property(e => e.CustomerNumber).IsUnicode(false);

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.PaymentRequestCustomers)
                    .HasForeignKey(d => d.DivisionID)
                    .HasConstraintName("FK_PaymentRequestCustomer_Division");

                entity.HasOne(d => d.Market)
                    .WithMany(p => p.PaymentRequestCustomers)
                    .HasForeignKey(d => d.MarketID)
                    .HasConstraintName("FK_PaymentRequestCustomer_Market");

                entity.HasOne(d => d.PaymentRequest)
                    .WithMany(p => p.PaymentRequestCustomers)
                    .HasForeignKey(d => d.PaymentRequestID)
                    .HasConstraintName("FK_PaymentRequestCustomer_PaymentRequest");
            });

            modelBuilder.Entity<PaymentRequestInvoice>(entity =>
            {
                entity.HasIndex(e => new { e.ArInvoiceID, e.PaymentRequestInvoiceID })
                    .HasName("IX_PaymentRequestInvoice_PaumentRequestInvoiceId");

                entity.HasIndex(e => new { e.ArInvoiceID, e.PaymentAmount, e.InvoiceNumber, e.PaymentRequestCustomerID })
                    .HasName("IX_PaymentRequestInvoice_PaymentRequestCustomerID");

                entity.HasIndex(e => new { e.PaymentRequestCustomerID, e.InvoiceNumber, e.PaymentAmount, e.ArInvoiceID })
                    .HasName("IX_PaymentRequestInvoice_ARInvoiceID");

                entity.Property(e => e.InvoiceNumber).IsUnicode(false);

                entity.HasOne(d => d.PaymentRequestCustomer)
                    .WithMany(p => p.PaymentRequestInvoices)
                    .HasForeignKey(d => d.PaymentRequestCustomerID)
                    .HasConstraintName("FK_PaymentRequestInvoice_PaymentRequestCustomer");
            });

            modelBuilder.Entity<PaymentResponse>(entity =>
            {
                entity.HasIndex(e => e.Decision);

                entity.HasIndex(e => new { e.PaymentResponseID, e.PaymentRequestID })
                    .HasName("UNQ_PaymentResponse_PaymentResponseID_PaymentRequestID")
                    .IsUnique();

                entity.HasIndex(e => new { e.PaymentResponseID, e.AuthorizeDateTimeUtc, e.AuthorizeTransactionReferenceIdentifier, e.Decision, e.AuthorizeAmount, e.PaymentCardName, e.RequestCardNumber, e.VoidRefundDateTimeUtc, e.PaymentRequestID })
                    .HasName("IX_PaymentResponse_PaymentRequestID2");

                entity.Property(e => e.AuthorizeTransactionReferenceIdentifier).IsUnicode(false);

                entity.Property(e => e.BillToCountry).IsUnicode(false);

                entity.Property(e => e.BillToPostalCode).IsUnicode(false);

                entity.Property(e => e.BillToState).IsUnicode(false);

                entity.Property(e => e.Decision).IsUnicode(false);

                entity.Property(e => e.Message).IsUnicode(false);

                entity.Property(e => e.PaymentAccountName).IsUnicode(false);

                entity.Property(e => e.PaymentCardName).IsUnicode(false);

                entity.Property(e => e.PaymentProcessorTransactionIdentifier).IsUnicode(false);

                entity.Property(e => e.PaymentStatusCode).IsUnicode(false);

                entity.Property(e => e.PaymentStatusMessage).IsUnicode(false);

                entity.Property(e => e.PaymentToken).IsUnicode(false);

                entity.Property(e => e.RequestAccountNumber).IsUnicode(false);

                entity.Property(e => e.RequestAccountType).IsUnicode(false);

                entity.Property(e => e.RequestCardExpiration).IsUnicode(false);

                entity.Property(e => e.RequestCardNumber).IsUnicode(false);

                entity.Property(e => e.RequestCardType).IsUnicode(false);

                entity.Property(e => e.RequestCheckNumber).IsUnicode(false);

                entity.Property(e => e.RequestPaymentMethod).IsUnicode(false);

                entity.Property(e => e.RequestRoutingNumber).IsUnicode(false);

                entity.Property(e => e.RequestTransactionTypeCode).IsUnicode(false);

                entity.Property(e => e.ResponseDataJson).IsUnicode(false);

                entity.HasOne(d => d.PaymentRequest)
                    .WithMany(p => p.PaymentResponses)
                    .HasForeignKey(d => d.PaymentRequestID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PaymentResponse_PaymentRequest");
            });

            modelBuilder.Entity<PaymentResponseDivision>(entity =>
            {
                entity.HasIndex(e => new { e.DivisionID, e.Decision, e.PaymentRequestID })
                    .HasName("IX_PaymentResponseDivision_DivisionID_Decision");

                entity.Property(e => e.AuthorizeTransactionReferenceIdentifier).IsUnicode(false);

                entity.Property(e => e.Decision).IsUnicode(false);

                entity.Property(e => e.PaymentProcessorTransactionIdentifier).IsUnicode(false);

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.PaymentResponseDivisions)
                    .HasForeignKey(d => d.DivisionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PaymentResponseDivision_Division");

                entity.HasOne(d => d.PaymentResponse)
                    .WithMany(p => p.PaymentResponseDivisions)
                    .HasForeignKey(d => d.PaymentResponseID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PaymentResponseDivision_PaymentResponse");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => new { e.FirstName, e.LastName, e.Email, e.Password, e.SecretQuestion, e.SecretAnswer, e.CreationDate, e.ExpirationDate, e.ActiveDirectory, e.CustomerGuid, e.LastEmailSentOn, e.Company, e.CustomerID, e.ForceNewPassword, e.LoginCount, e.LoginKey, e.PhoneNumber, e.LastLoginDateTimeUtc, e.UserName, e.UserTypeID, e.UserID })
                    .HasName("IX_UserId_UserType_UserNameWithEverything");

                entity.HasIndex(e => new { e.FirstName, e.LastName, e.Email, e.UserName, e.Password, e.SecretQuestion, e.SecretAnswer, e.CreationDate, e.ExpirationDate, e.UserTypeID, e.ActiveDirectory, e.CustomerGuid, e.LastEmailSentOn, e.Company, e.CustomerID, e.ForceNewPassword, e.LoginCount, e.LoginKey, e.PhoneNumber, e.LastLoginDateTimeUtc, e.UserID })
                    .HasName("IX_UserId_WithEverything");

                entity.Property(e => e.ApprovedBy).IsUnicode(false);

                entity.Property(e => e.Company).IsUnicode(false);

                entity.Property(e => e.CustomerGuid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CustomerID).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastLoginDateTimeUtc).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.LoginKey).IsUnicode(false);

                entity.Property(e => e.ModifiedBy).IsUnicode(false);

                entity.Property(e => e.NotifyNewUserAccessRequest).HasDefaultValueSql("((0))");

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.PhoneNumber).IsUnicode(false);

                entity.Property(e => e.SecretAnswer).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}