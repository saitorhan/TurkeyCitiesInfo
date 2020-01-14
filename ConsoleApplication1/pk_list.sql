USE [MetaDatas]
GO

/****** Object:  Table [dbo].[pk_list]    Script Date: 14.01.2020 16:27:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[pk_list](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[il] [nvarchar](100) NULL,
	[ilce] [nvarchar](100) NULL,
	[semt_bucak_belde] [nvarchar](100) NULL,
	[Mahalle] [nvarchar](100) NULL,
	[PK] [nvarchar](20) NULL,
 CONSTRAINT [PK_pk_list] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

