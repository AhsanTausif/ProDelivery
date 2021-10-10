INSERT INTO [dbo]. [Dealership]([Sender_Name],[Branch_Address],[Date_Of_Apply],[Money_Deposited],[Sender_Id],[Admin_Id])
VALUES	('Tausif','Dhaka,Bangladesh','2021-04-05',1000,1,1),
		('Shafique','Dhaka,Bangladesh','2021-02-03',500,2,1),
		('Abir','Dhaka,Bangladesh','2021-07-06',5000,3,1)

INSERT INTO [dbo]. [Dispatcher]([Rec_Name],[Sender_Name],[Branch_Name],[Description],[Rec_Id],[Sender_Id],[Admin_Id])
VALUES	('Tausif','Nishat','Dhaka','Instant Delivery',1,1,1),
		('Shafique','Rabbi','Dinajpur','Instant Delivery',2,2,1),
		('Abir','Jesmin','Khulna','Instant Delivery',3,3,1)

INSERT INTO [dbo]. [Consignment]([Shipper_Name],[Shipper_Phone],[Total_Weight],[Category],[No_Of_Items],[Description],[Branch_Name],[Branch_Address] ,[Rec_Name], [Rec_Address],[Branch_Id],[Rec_Id])
VALUES	('Niloy',01445678910,500,'Parcel',2,'Papers and Documents','Dhanmondi','Dhaka,Bangladesh','Ahsan','Dhaka,Bangladesh',1,1),
        ('Zaman',01745678950,100,'Parcel',1,'Papers and Documents','Shyamoli','Dhaka,Bangladesh','Nishat','Dhaka,Bangladesh',1,1),
		('Kamrul',01945677480,150,'Parcel',4,'Papers and Documents','Mirpur','Dhaka,Bangladesh','Zunayed','Dhaka,Bangladesh',1,1)
          