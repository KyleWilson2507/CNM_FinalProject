use FakeProductIden


--ProductTypes--
insert into ProductTypes values ('TY001', 'Novel'),
								('TY002', 'Comic'),
								('TY003', 'Reference'),
								('TY004', 'Pupil book'),
								('TY005', 'Cook book')

--Branches--
insert into Companies values ('CMP001', 'Oxford University Press', 'UK'),
							 ('CMP002', 'Shueisha', 'Japan'),
							 ('CMP003', 'McGraw-Hill Education', 'USA'),
							 ('CMP004', 'NXB Tre', 'VietNam'),
							 ('CMP005', 'Kim Dong', 'VietNam')

--Products--
insert into Products values ('PR001', 'English for you', 'CMP001', 'TY004', 'UK', 190000),
							('PR002', 'Spelling Game', 'CMP001', 'TY004', 'UK', 500000),
							('PR003', 'Attack On Titan', 'CMP002', 'TY002', 'Japan', 260000),
							('PR004', 'Doraemon', 'CMP005', 'TY002', 'Vietnam', 170000),
							('PR005', 'Twilight', 'CMP004', 'TY001', 'Vietnam', 480000),
							('PR006', 'Life of Albert Einstein', 'CMP003', 'TY003', 'USA', 55000),
							('PR007', 'Les Miserables', 'CMP004', 'TY001', 'Vietnam', 400000),
							('PR008', 'Hunter X Hunter', 'CMP002', 'TY002', 'Japan', 320000),
							('PR009', 'Vegan food for your soul', 'CMP004', 'TY005', 'VietNam', 150000),
							('PR010', '1001 Festival Delights', 'CMP005', 'TY005', 'VietNam', 900000)
							