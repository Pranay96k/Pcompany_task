USE [Pcompany_task]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 07-04-2023 14:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Category] [nvarchar](50) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product_table]    Script Date: 07-04-2023 14:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_table](
	[Product_id] [int] IDENTITY(1,1) NOT NULL,
	[Category_id] [int] NULL,
	[Product_name] [nvarchar](50) NULL,
	[Price] [decimal](10, 2) NULL,
 CONSTRAINT [PK_Product_table] PRIMARY KEY CLUSTERED 
(
	[Product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Product_table]  WITH CHECK ADD  CONSTRAINT [FK_Product_table_Category] FOREIGN KEY([Category_id])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Product_table] CHECK CONSTRAINT [FK_Product_table_Category]
GO
/****** Object:  StoredProcedure [dbo].[sp_category_report]    Script Date: 07-04-2023 14:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_category_report]
as
 begin
 select * from Category
 end
GO
/****** Object:  StoredProcedure [dbo].[sp_product_report]    Script Date: 07-04-2023 14:49:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE procedure [dbo].[sp_product_report]
 as
 begin
 select p.Product_id, p.Product_name, p.Category_id, c.Category as[Category_name] from Category C
 inner join Product_table P on
 c.Id=p.Category_id


 end
GO
