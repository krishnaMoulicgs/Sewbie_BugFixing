
SET IDENTITY_INSERT [dbo].[Nop_Order] ON
INSERT [dbo].[Nop_Order] ([OrderID], [OrderGUID], [CustomerID], [CustomerLanguageID], [CustomerTaxDisplayTypeID], [CustomerIP], [OrderSubtotalInclTax], [OrderSubtotalExclTax], [OrderSubTotalDiscountInclTax], [OrderSubTotalDiscountExclTax], [OrderShippingInclTax], [OrderShippingExclTax], [PaymentMethodAdditionalFeeInclTax], [PaymentMethodAdditionalFeeExclTax], [TaxRates], [OrderTax], [OrderTotal], [RefundedAmount], [OrderDiscount], [OrderSubtotalInclTaxInCustomerCurrency], [OrderSubtotalExclTaxInCustomerCurrency], [OrderSubTotalDiscountInclTaxInCustomerCurrency], [OrderSubTotalDiscountExclTaxInCustomerCurrency], [OrderShippingInclTaxInCustomerCurrency], [OrderShippingExclTaxInCustomerCurrency], [PaymentMethodAdditionalFeeInclTaxInCustomerCurrency], [PaymentMethodAdditionalFeeExclTaxInCustomerCurrency], [TaxRatesInCustomerCurrency], [OrderTaxInCustomerCurrency], [OrderTotalInCustomerCurrency], [OrderDiscountInCustomerCurrency], [CustomerCurrencyCode], [CheckoutAttributeDescription], [CheckoutAttributesXML], [OrderWeight], [AffiliateID], [OrderStatusID], [AllowStoringCreditCardNumber], [CardType], [CardName], [CardNumber], [MaskedCreditCardNumber], [CardCVV2], [CardExpirationMonth], [CardExpirationYear], [PaymentMethodID], [PaymentMethodName], [AuthorizationTransactionID], [AuthorizationTransactionCode], [AuthorizationTransactionResult], [CaptureTransactionID], [CaptureTransactionResult], [SubscriptionTransactionID], [PurchaseOrderNumber], [PaymentStatusID], [PaidDate], [BillingFirstName], [BillingLastName], [BillingPhoneNumber], [BillingEmail], [BillingFaxNumber], [BillingCompany], [BillingAddress1], [BillingAddress2], [BillingCity], [BillingStateProvince], [BillingStateProvinceID], [BillingZipPostalCode], [BillingCountry], [BillingCountryID], [ShippingStatusID], [ShippingFirstName], [ShippingLastName], [ShippingPhoneNumber], [ShippingEmail], [ShippingFaxNumber], [ShippingCompany], [ShippingAddress1], [ShippingAddress2], [ShippingCity], [ShippingStateProvince], [ShippingStateProvinceID], [ShippingZipPostalCode], [ShippingCountry], [ShippingCountryID], [ShippingMethod], [ShippingRateComputationMethodID], [ShippedDate], [TrackingNumber], [DeliveryDate], [VatNumber], [Deleted], [CreatedOn]) VALUES (1, N'6b75e4ea-abbc-4785-9641-208b57aecbdc', 37, 7, 2, N'69.248.109.13', 50.0000, 50.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, N'0:0;   ', 0.0000, 50.0000, 0.0000, 0.0000, 50.0000, 50.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, 0.0000, N'0:0;   ', 0.0000, 50.0000, 0.0000, N'USD', N'', N'', CAST(0.0000 AS Decimal(18, 4)), 0, 10, 0, N'', N'', N'', N'', N'', N'', N'', 43, N'Paypal Peer Payment', N'', N'', N'', N'', N'', N'', N'', 10, NULL, N'John', N'Smith', N'12345678', N'admin@yourStore.com', N'', N'Nop Solutions', N'21 West 52nd Street', N'', N'New York', N'New York', 41, N'10021', N'United States', 1, 10, N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', 0, N'', N'', 0, N'', 0, NULL, N'', NULL, N'', 1, CAST(0x00009FB000602E7B AS DateTime))
SET IDENTITY_INSERT [dbo].[Nop_Order] OFF


SET IDENTITY_INSERT [dbo].[Nop_NewsLetterSubscription] ON

SET IDENTITY_INSERT [dbo].[Nop_NewsLetterSubscription] OFF


SET IDENTITY_INSERT [dbo].[Nop_Customer] ON
INSERT [dbo].[Nop_Customer] ([CustomerID], [CustomerGUID], [Email], [Username], [PasswordHash], [SaltKey], [AffiliateID], [BillingAddressID], [ShippingAddressID], [LastPaymentMethodID], [LastAppliedCouponCode], [GiftCardCouponCodes], [CheckoutAttributes], [LanguageID], [CurrencyID], [TaxDisplayTypeID], [IsTaxExempt], [IsAdmin], [IsGuest], [IsVendor], [IsForumModerator], [TotalForumPosts], [Signature], [AdminComment], [Active], [Deleted], [RegistrationDate], [TimeZoneID], [AvatarID], [DateOfBirth]) VALUES (37, N'18c4f7dc-2964-41db-9611-8a313bf672d3', N'siteadmin@sewbie.com', N'siteadmin@sewbie.com', N'16631C8A2A55CC2E6EC47D51A951232AB4D253D9', N'alo50FM=', 0, 239, 77, 0, N'', N'', N'', 7, 1, 1, 0, 1, 0, 1, 1, 0, N'', N'', 1, 0, CAST(0x00009B3600C2192F AS DateTime), N'', 0, CAST(0x0000769B00000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[Nop_Customer] OFF

SET IDENTITY_INSERT [dbo].[Nop_Product] ON
INSERT [dbo].[Nop_Product] ([ProductId], [Name], [ShortDescription], [FullDescription], [AdminComment], [TemplateID], [ShowOnHomePage], [MetaKeywords], [MetaDescription], [MetaTitle], [SEName], [AllowCustomerReviews], [AllowCustomerRatings], [RatingSum], [TotalRatingVotes], [Published], [Deleted], [Activated], [CreatedOn], [UpdatedOn]) VALUES (96, N'Logo Cap', N'Kana Signature Logo Cap', N'<p>Kana Signature Logo Cap&nbsp;</p>', N'', 5, 0, N'', N'', N'', N'', 1, 1, 0, 0, 1, 1, 1, CAST(0x00009F4A013CA04A AS DateTime), CAST(0x00009F4A014751EC AS DateTime))
SET IDENTITY_INSERT [dbo].[Nop_Product] OFF

SET IDENTITY_INSERT [dbo].[Nop_Picture] ON
INSERT [dbo].[Nop_Picture] ([PictureID], [PictureBinary], [IsNew], [MimeType]) VALUES (1, 0x, 0, N'image/jpeg')
SET IDENTITY_INSERT [dbo].[Nop_Picture] OFF

--Product Pictures

--Product Picture mapping

SET IDENTITY_INSERT [dbo].[Nop_Address] ON
INSERT [dbo].[Nop_Address] ([AddressId], [CustomerID], [IsBillingAddress], [FirstName], [LastName], [PhoneNumber], [Email], [FaxNumber], [Company], [Address1], [Address2], [City], [StateProvinceID], [ZipPostalCode], [CountryID], [CreatedOn], [UpdatedOn]) VALUES (77, 37, 0, N'Hazel', N'Gumban', N'2017770029', N'info@sewbie.com', N'', N'Sewbie LLC', N'177 Audubon Ave', N'', N'Jersey City', 39, N'07305', 1, CAST(0x00009BE3014ADFF5 AS DateTime), CAST(0x0000A0780024E3BA AS DateTime))
SET IDENTITY_INSERT [dbo].[Nop_Address] OFF

SET IDENTITY_INSERT [dbo].[Nop_CustomerAttribute] ON
INSERT [dbo].[Nop_CustomerAttribute] ([CustomerAttributeId], [CustomerId], [Key], [Value]) VALUES (181, 37, N'Gender', N'M')
INSERT [dbo].[Nop_CustomerAttribute] ([CustomerAttributeId], [CustomerId], [Key], [Value]) VALUES (182, 37, N'FirstName', N'Neil')
INSERT [dbo].[Nop_CustomerAttribute] ([CustomerAttributeId], [CustomerId], [Key], [Value]) VALUES (183, 37, N'LastName', N'Fajardo')
INSERT [dbo].[Nop_CustomerAttribute] ([CustomerAttributeId], [CustomerId], [Key], [Value]) VALUES (185, 37, N'Company', N'SEWBIE')
INSERT [dbo].[Nop_CustomerAttribute] ([CustomerAttributeId], [CustomerId], [Key], [Value]) VALUES (186, 37, N'StreetAddress', N'99 Gillis Place')
INSERT [dbo].[Nop_CustomerAttribute] ([CustomerAttributeId], [CustomerId], [Key], [Value]) VALUES (187, 37, N'StreetAddress2', N'')
INSERT [dbo].[Nop_CustomerAttribute] ([CustomerAttributeId], [CustomerId], [Key], [Value]) VALUES (188, 37, N'ZipPostalCode', N'07094')
INSERT [dbo].[Nop_CustomerAttribute] ([CustomerAttributeId], [CustomerId], [Key], [Value]) VALUES (189, 37, N'City', N'Secaucus')
INSERT [dbo].[Nop_CustomerAttribute] ([CustomerAttributeId], [CustomerId], [Key], [Value]) VALUES (190, 37, N'PhoneNumber', N'2014720066')
INSERT [dbo].[Nop_CustomerAttribute] ([CustomerAttributeId], [CustomerId], [Key], [Value]) VALUES (192, 37, N'CountryID', N'1')
INSERT [dbo].[Nop_CustomerAttribute] ([CustomerAttributeId], [CustomerId], [Key], [Value]) VALUES (193, 37, N'StateProvinceID', N'39')
INSERT [dbo].[Nop_CustomerAttribute] ([CustomerAttributeId], [CustomerId], [Key], [Value]) VALUES (312, 37, N'FaxNumber', N'')
INSERT [dbo].[Nop_CustomerAttribute] ([CustomerAttributeId], [CustomerId], [Key], [Value]) VALUES (313, 37, N'UseRewardPointsDuringCheckout', N'False')
INSERT [dbo].[Nop_CustomerAttribute] ([CustomerAttributeId], [CustomerId], [Key], [Value]) VALUES (314, 61, N'Gender', N'F')
SET IDENTITY_INSERT [dbo].[Nop_CustomerAttribute] OFF


--Order Note


SET IDENTITY_INSERT [dbo].[Nop_Product_Manufacturer_Mapping] ON
SET IDENTITY_INSERT [dbo].[Nop_Product_Manufacturer_Mapping] OFF

SET IDENTITY_INSERT [dbo].[Nop_Product_Category_Mapping] ON
SET IDENTITY_INSERT [dbo].[Nop_Product_Category_Mapping] OFF
