INSERT INTO [dbo]. [Branch]([Branch_Name],[Branch_Address],[Branch_Phone],[Pincode],[Request_Id],[Rec_Id],[Sender_Id],[Admin_Id])
VALUES	('Dhaka','Dhaka,Bangladesh',12345678910,1207,11,1,1,1),
		('Dinajpur','Dinajpur,Bangladesh',12345678910,1051,12,1,1,1),
		('Khulna','Khulna,Bangladesh',12345678910,13021,13,1,1,1)

INSERT INTO [dbo]. [Dispatcher]([Rec_Name],[Sender_Name],[Branch_Name],[Description],[Rec_Id],[Sender_Id],[Admin_Id])
VALUES	('Ayon','Tausif','Dhaka','Instant Delivery',1,1,1),
		('Nishat','Zunayed','Dinajpur','Instant Delivery',5,2,1),
		('Nishat','Tausif5','Khulna','Instant Delivery',3,7,1)

INSERT INTO [dbo]. [Consignment]([Shipper_Name],[Shipper_Phone],[Total_Weight],[Category],[No_Of_Items],[Description],[Branch_Name],[Branch_Address] ,[Rec_Name], [Rec_Address],[Branch_Id],[Rec_Id])
VALUES	('Niloy',01445678910,500,'Parcel',2,'Papers and Documents','Dhaka','Dhaka,Bangladesh','Ayon','Dhaka,Bangladesh',1,4),
        ('Zaman',01745678950,100,'Parcel',1,'Papers and Documents','Dinajpur','Dinajpur,Bangladesh','Nishat','Dhaka,Bangladesh',2,5),
		('Kamrul',01945677480,150,'Parcel',4,'Papers and Documents','Khulna','Khulna,Bangladesh','Tausif4','Dhaka,Bangladesh',3,7)
          