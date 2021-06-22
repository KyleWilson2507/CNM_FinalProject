use master

if DB_ID('FakeProductIden') is not null drop database FakeProductIden

create database FakeProductIden

use FakeProductIden

create table Products (
	pr_id varchar(50) not null,
	pr_name nvarchar(255),
	pr_cmp nvarchar(50),
	pr_type nvarchar(50),
	pr_origin nvarchar(255),
	pr_price float,
	primary key(pr_id),
);

create table Companies (
	cmp_id nvarchar(50) not null,
	cmp_name nvarchar(255),
	cmp_headquarter nvarchar(255),
	primary key(cmp_id),
);

create table ProductTypes (
	ty_id nvarchar(50) not null,
	ty_name nvarchar(255),
	primary key(ty_id),
);

alter table Products add constraint fk_product_branch foreign key (pr_cmp) references Companies(cmp_id)
alter table Products add constraint fk_product_producttype foreign key (pr_type) references ProductTypes(ty_id)

