-- Insert-only seed script (max 100 rows per table)
-- Target: modernization PostgreSQL schema
-- No CREATE/ALTER/UPDATE statements included.

BEGIN;

INSERT INTO "Airplanes" ("AirplaneId", "Type", "NumSeats", "TotalFuel") VALUES
  (1, '737-200', 130, 26),
  (2, 'A320', 150, 28),
  (3, 'A340', 230, 35),
  (4, '737-300', 200, 30),
  (5, '737-200', 130, 26),
  (6, '737-600', 300, 40),
  (7, 'A340', 230, 35),
  (8, 'A321', 180, 28),
  (9, 'A321', 180, 28),
  (10, 'A330', 260, 35)
ON CONFLICT ("AirplaneId") DO NOTHING;

INSERT INTO "Airports" ("AirportId", "Name", "Address", "City", "Country", "ZipCode") VALUES
  (1, 'Aéroport Paris-Charles de Gaulles', 'Aéroport de Paris Rossy Charles de Gaulle', 'Rossy', 'France', '95711'),
  (2, 'Aéroport Bordeaux - Mérignac', 'Avenue René Cassin', 'Mérignac', 'France', '33700'),
  (3, 'Aéroport Léonard-de-Vinci de Rome Fiumicino', 'Via Arturo Ferrarin, 2', 'Fiumicino', 'Italie', '00054'),
  (4, 'Airoport international de Lisbonne Humberto Delegado', 'Alameda das Comunidades Portuguesas, 1700-111', 'Lisbonne', 'Portugal', '1700'),
  (5, 'Heathrow airport', 'Longford TW6', 'Londres', 'Royaume Unis', 'TW6 1QT'),
  (6, 'Aéroport d''Amsterdam-Schiphol', 'Evert van de Beekstraat 202, 1118', 'Amsterdam', 'Hollande', '1118'),
  (7, 'Aéroport de Francfort-sur-le-Main', 'Aéroport de Francfort 60547 Frankfurt am Main Allemagne', 'Frankfurt', 'Allemagne', '60549'),
  (8, 'Aéroport Atatürk d''Istanbul', 'Yeşilköy, 34149 Bakırköy', 'İstanbul', 'Turquie', '0000'),
  (9, 'Aéroport Adolfo Suárez Madrid-Barajas', 'Av de la Hispanidad, s/n', 'Madrid', 'Espagne', '28042')
ON CONFLICT ("AirportId") DO NOTHING;

INSERT INTO "Departments" ("DeptId", "Name", "ManagerId") VALUES
  (1, 'ceo', NULL),
  (2, 'commander', NULL),
  (3, 'copilote', NULL),
  (4, 'Flight Attendant', NULL),
  (5, 'Human resources', NULL),
  (6, 'Suport IT', NULL),
  (7, 'Sales', NULL),
  (8, 'Legal', NULL),
  (9, 'Schedule', NULL)
ON CONFLICT ("DeptId") DO NOTHING;

INSERT INTO "Employees" ("EmpId", "FirstName", "LastName", "Addre", "City", "ZipCode", "Telephone", "Email", "AdmiDate", "Salary", "Password", "DeptId") VALUES
  (1, 'Mirelle', 'Thurstance', '9270 Esch Parkway', 'Khonj', '87100', '3106069183', 'mthurstance0@psu.edu', '2022-01-15', 9691.38, '12345', 5),
  (2, 'Torey', 'Fache', '887 Dahle Pass', 'Malanville', '94300', '7933458250', 'tfache1@csmonitor.com', '2006-07-04', 8629.20, '12345', 6),
  (3, 'Milzie', 'Giannasi', '025 Novick Place', 'Al Kiswah', '75200', '4039935394', 'mgiannasi2@columbia.edu', '2014-06-18', 2976.68, '12345', 2),
  (4, 'Ruthy', 'Ahlf', '02 Roth Junction', 'Cowansville', '33100', '8207447359', 'rahlf3@skyrock.com', '2010-04-16', 2923.22, '12345', 3),
  (5, 'Hyacinth', 'People', '423 Pierstorff Plaza', 'Barrhead', '12400', '4974705800', 'hpeople4@google.fr', '2006-01-27', 7555.44, '12345', 4),
  (6, 'Bonnie', 'Tissier', '830 Merchant Point', 'Floirac', '33274', '3353128122', 'btissier5@deviantart.com', '2012-12-03', 4895.81, '12345', 7),
  (7, 'Vera', 'Alcalde', '5649 New Castle Road', 'Itaúna', '45700', '9565256171', 'valcalde6@ebay.com', '2012-07-19', 3503.69, '12345', 4),
  (8, 'Adda', 'Hulles', '3919 Crowley Hill', 'Yanling', '23910', '1578072173', 'ahulles7@globo.com', '2019-11-20', 8543.95, '12345', 4),
  (9, 'Orelia', 'Hamner', '8406 Columbus Circle', 'Mertzig', '34000', '8679088527', 'ohamner8@seattletimes.com', '2005-08-20', 4924.21, '12345', 4),
  (10, 'Clayborne', 'Ifill', '9 Riverside Trail', 'Shuiji', '56120', '3849176605', 'cifill9@adobe.com', '2019-04-17', 5547.58, '12345', 4),
  (11, 'Tatum', 'Aingell', '616 Mallory Way', 'Qīr Moāv', '96100', '5018542308', 'taingella@cisco.com', '2001-03-31', 2195.52, '12345', 4),
  (12, 'Yelena', 'Challicum', '5605 Dorton Place', 'Votkinsk', '12100', '1923838931', 'ychallicumb@i2i.jp', '2010-01-01', 1711.43, '12345', 3),
  (13, 'Erhart', 'Hanniger', '2 Park Meadow Circle', 'Sudikampir', '78000', '2558276768', 'ehannigerc@a8.net', '2018-09-11', 2253.25, '12345', 5),
  (14, 'Josiah', 'Selvey', '1889 Washington Drive', 'Siwa Oasis', '43010', '5575429140', 'jselveyd@google.cn', '2007-08-29', 6246.82, '12345', 2),
  (15, 'Lurette', 'Vakhrushin', '1191 Sheridan Avenue', 'Sumurasin', '12345', '2784194274', 'lvakhrushine@mysql.com', '2010-03-02', 3083.94, '12345', 7),
  (16, 'Dewitt', 'Ugoni', '62196 Lighthouse Bay Trail', 'Campo', '54321', '7414544886', 'dugonif@bloglines.com', '2017-05-08', 1286.88, '12345', 7),
  (17, 'Stern', 'Duferie', '5500 Loeprich Parkway', 'Lumatil', '74012', '4064797891', 'sduferieg@columbia.edu', '2017-12-05', 5934.57, '12345', 8),
  (18, 'Tiphany', 'Spollen', '8 Prentice Avenue', 'Lukićevo', '94100', '7605881452', 'tspollenh@indiatimes.com', '2021-07-10', 8413.66, '12345', 2),
  (19, 'Devondra', 'Nolot', '863 Miller Point', 'Ponta Porã', '79900', '8391069450', 'dnoloti@shutterfly.com', '2003-08-30', 3051.90, '12345', 7),
  (20, 'Normie', 'Linge', '92123 Service Lane', 'Calatrava', '61260', '5715567680', 'nlingej@china.com.cn', '2009-07-27', 2869.69, '12345', 3),
  (21, 'Aldo', 'Yetton', '6136 Dennis Pass', 'Frontignan', '34114', '2382017996', 'ayettonk@unesco.org', '2018-07-10', 2127.45, '12345', 9),
  (22, 'Celestyna', 'Widdocks', '2 Ronald Regan Lane', 'Iballë', '75010', '5126426102', 'cwiddocksl@admin.ch', '2020-05-14', 6291.12, '12345', 9),
  (23, 'Yoshi', 'Verissimo', '06999 Eliot Avenue', 'Karaundi', '30001', '4753361993', 'yverissimom@sakura.ne.jp', '2010-06-17', 9140.19, '12345', 9),
  (24, 'Enriqueta', 'Eltone', '7027 Darwin Way', 'Preiļi', '11000', '9979450624', 'eeltonen@youtube.com', '2009-01-15', 4360.80, '12345', 4),
  (25, 'Andy', 'Bumfrey', '4 Norway Maple Avenue', 'Montongtebolak', '65300', '1489100356', 'abumfreyo@jugem.jp', '2012-03-24', 8147.97, '12345', 4),
  (26, 'Sheffy', 'Neillans', '59712 Browning Lane', 'Jacareí', '12300', '7342431413', 'sneillansp@privacy.gov.au', '2014-05-19', 5097.39, '12345', 4),
  (27, 'Maynord', 'Shallcroff', '8707 Thierer Terrace', 'Wolofeo', '87654', '6277235258', 'mshallcroffq@last.fm', '2007-08-03', 1931.42, '12345', 6),
  (28, 'Iver', 'O''Dowd', '40 Mesta Center', 'Cipatujah', '35790', '7319499910', 'iodowdr@weebly.com', '2019-04-25', 3974.86, '12345', 6),
  (29, 'Grissel', 'Buxcy', '99009 Fairfield Circle', 'Belčišta', '63440', '7176920722', 'gbuxcys@sfgate.com', '2010-06-02', 2278.09, '12345', 1),
  (30, 'Egbert', 'Humbell', '4 Doe Crossing Road', 'Starokucherganovka', '41647', '5235600487', 'ehumbellt@amazon.co.uk', '2021-02-26', 4620.52, '12345', 7),
  (31, 'Robert', 'Antony', '24 rue de l abbaye', 'Limoges', '87000', '0700004444', 'robertantony@gmail.com', '2012-04-04', 5000.98, '12345', 4),
  (32, 'Marguarette', 'Rose', '12 square du lord', 'Paris', '75000', '0611112222', 'marguaretterose@gmail.com', '2018-10-21', 4500.00, '12345', 4),
  (33, 'Carolina', 'Pilolo', '123 rue vialube', 'Anthony', '94200', '0722334455', 'carolinapilolo@gmail.com', '2012-04-04', 5000.98, '12345', 4),
  (34, 'Andre', 'Rodriigues', '199 rue de mars', 'Lyon', '23000', '0712345678', 'andrerodrigues@gmail.com', '2010-10-04', 7250.98, '12345', 2),
  (35, 'Joana', 'Marley', '3456 avenue du midi', 'Nanterre', '92004', '0709876543', 'joanadumidi@gmail.com', '2020-05-12', 6000.98, '12345', 3),
  (36, 'Antonieta', 'Albuquerque', '98 allé de passage', 'Saint Denis', '95000', '0733445566', 'antonietaalbu@gmail.com', '2018-01-04', 4333.98, '12345', 4),
  (37, 'Marcio', 'Slat', '202 rue d hier', 'Rossy', '95100', '0666666666', 'marcioslat@gmail.com', '2019-02-23', 4000.98, '12345', 4),
  (38, 'Thiago', 'Robespierre', '1789 rue de la revolution', 'Bastille', '89000', '0789898989', 'thiagorob@gmail.com', '1989-09-09', 4000.98, '12345', 4),
  (39, 'Helena', 'Kerustch', '22 rue de la maternelle', 'Limoges', '87000', '0700004444', 'helenakeke@gmail.com', '2020-02-02', 4000.98, '12345', 4)
ON CONFLICT ("EmpId") DO NOTHING;

INSERT INTO "Passengers" ("ClientId", "FirstName", "LastName", "Address", "City", "Country", "ZipCode", "Telephone", "Email") VALUES
  (1, 'Carmita', 'Dallender', '2 Glacier Hill Drive', 'Sambava', 'Madagascar', '55910-167', '9636523490', 'cdallender0@4shared.com'),
  (2, 'Julianne', 'Sissot', '58322 Fairfield Drive', 'Swift Current', 'Canada', '45167-040', '5931887564', 'jsissot1@deviantart.com'),
  (3, 'Dosi', 'Chazette', '76967 Independence Circle', 'Abbeville', 'France', '68026-105', '1349918173', 'dchazette2@woothemes.com'),
  (4, 'Augustine', 'Utley', '188 Pleasure Circle', 'Biancheng', 'China', '43063-417', '8528350428', 'autley3@diigo.com'),
  (5, 'Markos', 'Aronovich', '59136 Quincy Avenue', 'Qorao zak', 'Uzbekistan', '55316-065', '9176258112', 'maronovich4@jiathis.com'),
  (6, 'Edeline', 'Firle', '67216 Park Meadow Junction', 'Ferreira do Alentejo', 'Portugal', '55714-4404', '7898890638', 'efirle5@archive.org'),
  (7, 'Eunice', 'Ritmeier', '70370 Main Trail', 'Paldit', 'Philippines', '29943-004', '3699725238', 'eritmeier6@ted.com'),
  (8, 'Lilia', 'Aymes', '3 Farwell Alley', 'Kojagete', 'Indonesia', '23155-126', '6025617900', 'laymes7@noaa.gov'),
  (9, 'Tristan', 'Boays', '451 Ohio Trail', 'Zaslawye', 'Belarus', '11822-0408', '6674977218', 'tboays8@umn.edu'),
  (10, 'Sigismund', 'Chad', '319 Schlimgen Road', 'Logovskoye', 'Russia', '62011-0131', '3751864546', 'schad9@meetup.com'),
  (11, 'Mord', 'Daniau', '04 Welch Circle', 'Umburarameha', 'Indonesia', '55301-009', '3406430297', 'mdaniaua@go.com'),
  (12, 'Ariella', 'Glenton', '58030 Spenser Parkway', 'Düsseldorf', 'Germany', '13668-083', '1313101228', 'aglentonb@taobao.com'),
  (13, 'Daloris', 'Stribling', '65 Merchant Place', 'Serawai', 'Indonesia', '61314-012', '3078948723', 'dstriblingc@github.com'),
  (14, 'Carla', 'Challin', '4 Buhler Crossing', 'Rundeng', 'Indonesia', '61010-1122', '7408974615', 'cchallind@behance.net'),
  (15, 'Ardisj', 'Paradin', '368 Mitchell Crossing', 'Lingion', 'Philippines', '41520-312', '9181773189', 'aparadine@ycombinator.com'),
  (16, 'Lauritz', 'Duckham', '33868 Elmside Center', 'Knyaginino', 'Russia', '0574-0134', '5941633650', 'lduckhamf@japanpost.jp'),
  (17, 'Modestia', 'Blindmann', '0846 5th Way', 'Novosemeykino', 'Russia', '53942-223', '4001150548', 'mblindmanng@narod.ru'),
  (18, 'Mersey', 'Maffulli', '7605 Ridge Oak Junction', 'Carregal', 'Portugal', '60432-065', '6904780394', 'mmaffullih@shop-pro.jp'),
  (19, 'Mikaela', 'Belshaw', '24 Esch Pass', 'Rinang', 'China', '59779-346', '9082628221', 'mbelshawi@alibaba.com'),
  (20, 'Timofei', 'Verrell', '7352 Burning Wood Alley', 'Shaba', 'China', '49348-956', '9833083223', 'tverrellj@nationalgeographic.com'),
  (21, 'Tiler', 'Dacke', '186 Anniversary Place', 'Hoopstad', 'South Africa', '0548-5631', '2862361934', 'tdackek@loc.gov'),
  (22, 'Layney', 'Gomery', '4651 Maple Street', 'Chelm', 'Poland', '63667-242', '1748990803', 'lgomeryl@clickbank.net'),
  (23, 'Aleksandr', 'Dillintone', '2658 Logan Road', 'Sukawaris', 'Indonesia', '65044-2599', '8266517284', 'adillintonem@cnn.com'),
  (24, 'Lennard', 'Giffard', '697 Hollow Ridge Junction', 'Nantes', 'France', '66336-342', '5664153548', 'lgiffardn@odnoklassniki.ru'),
  (25, 'Xena', 'Crotch', '93 Golf Course Park', 'Moulins', 'France', '59048-000', '7945697286', 'xcrotcho@eventbrite.com'),
  (26, 'Ellery', 'Bernth', '66294 Butternut Crossing', 'Kitami', 'Japan', '36987-1937', '7321784954', 'ebernthp@delicious.com'),
  (27, 'Kathrine', 'Rizzone', '993 Shelley Park', 'Caballococha', 'Peru', '14783-329', '7829777237', 'krizzoneq@opera.com'),
  (28, 'Kristos', 'Boase', '8 Hansons Drive', 'Hexi', 'China', '53329-970', '3245211069', 'kboaser@wikia.com'),
  (29, 'Hamid', 'Busch', '79471 Holmberg Alley', 'Guicheng', 'China', '55648-101', '6062389520', 'hbuschs@cocolog-nifty.com'),
  (30, 'Marketa', 'Tuddenham', '704 Summerview Avenue', 'Czudec', 'Poland', '68770-101', '4142523067', 'mtuddenhamt@usa.gov'),
  (31, 'Jerrie', 'Paolinelli', '06638 Bartelt Junction', 'Zhanlong', 'China', '63323-477', '4111597440', 'jpaolinelliu@google.ru'),
  (32, 'Angel', 'Shepeard', '85749 Lukken Drive', 'Magdaleno Aguilar', 'Mexico', '63736-594', '8114045866', 'ashepeardv@weebly.com'),
  (33, 'Falito', 'O Cosgra', '325 Springview Street', 'Hue', 'Vietnam', '68180-284', '4828428446', 'focosgraw@parallels.com'),
  (34, 'Kitti', 'Quodling', '9240 Steensland Pass', 'Guisijan', 'Philippines', '41250-693', '8113631530', 'kquodlingx@yandex.ru'),
  (35, 'Liliane', 'Begwell', '6 Stone Corner Parkway', 'Itsandzéni', 'Comoros', '64092-870', '5483717198', 'lbegwelly@technorati.com'),
  (36, 'Haleigh', 'Sandland', '22347 Lakewood Parkway', 'Si erbao', 'China', '63148-241', '1012832189', 'hsandlandz@elegantthemes.com'),
  (37, 'Knox', 'Andrasch', '75 Ryan Avenue', 'Lela', 'Indonesia', '63323-326', '7185892037', 'kandrasch10@vkontakte.ru'),
  (38, 'Tim', 'Suddards', '8 Scoville Crossing', 'Dongfanghong', 'China', '36987-1926', '7144813143', 'tsuddards11@hud.gov'),
  (39, 'Arvy', 'Ambrosetti', '322 Norway Maple Street', 'Piduhe', 'China', '65437-035', '7244058741', 'aambrosetti12@shutterfly.com'),
  (40, 'Burl', 'Race', '1622 Roth Road', 'Barrancas', 'Colombia', '52544-497', '8384391958', 'brace13@bbc.co.uk'),
  (41, 'Jocelyne', 'Maple', '54443 Superior Plaza', 'Washington', 'United States', '60681-1807', '2025659029', 'jmaple14@seattletimes.com'),
  (42, 'Kayla', 'Varvara', '7863 Center Court', 'Prnjavor', 'Serbia', '37012-490', '2686422189', 'kvarvara15@illinois.edu'),
  (43, 'Temp', 'Ioannou', '1 Mendota Pass', 'Norrköping', 'Sweden', '57520-1111', '2614775551', 'tioannou16@nytimes.com'),
  (44, 'Rollo', 'Berntssen', '61 Ridge Oak Hill', 'Knoxville', 'United States', '56062-535', '8658697527', 'rberntssen17@si.edu'),
  (45, 'Buddy', 'Britzius', '5566 Lighthouse Bay Circle', 'Las Trojes', 'Honduras', '49999-411', '6348403790', 'bbritzius18@shutterfly.com'),
  (46, 'Garrot', 'Thynn', '193 Sunfield Crossing', 'Wukou', 'China', '51141-0066', '8679228866', 'gthynn19@dot.gov'),
  (47, 'Adina', 'Lownds', '6996 Dorton Avenue', 'Taocun', 'China', '75939-9876', '5423141006', 'alownds1a@home.pl'),
  (48, 'Sidoney', 'Blowes', '2410 Kings Alley', 'Yeniköy', 'Turkey', '37205-117', '2651771608', 'sblowes1b@cdc.gov'),
  (49, 'Kellina', 'Etock', '4470 Monterey Point', 'Cerklje na Gorenjskem', 'Slovenia', '49999-982', '6571611421', 'ketock1c@oakley.com'),
  (50, 'Fin', 'Robberts', '9817 Del Sol Alley', 'Dongjia', 'China', '0280-1197', '1362718614', 'frobberts1d@ow.ly'),
  (51, 'Derrick', 'Masic', '5319 Valley Edge Center', 'Shangyanzhuang', 'China', '12546-613', '1679932822', 'dmasic1e@plala.or.jp'),
  (52, 'Konstance', 'Pawelski', '82293 Golf Course Trail', 'Campraksanta', 'Indonesia', '63629-4470', '9034777602', 'kpawelski1f@wikispaces.com'),
  (53, 'Cesar', 'Seldner', '7383 Spenser Court', 'Qingshui', 'China', '63323-323', '8236617495', 'cseldner1g@moonfruit.com'),
  (54, 'Fern', 'Skeemor', '91 Barby Lane', 'Bolembre', 'Portugal', '63304-311', '4285204169', 'fskeemor1h@japanpost.jp'),
  (55, 'Milissent', 'Whitcher', '4255 Sheridan Street', 'Rojas', 'Argentina', '57520-0322', '2743203165', 'mwhitcher1i@xing.com'),
  (56, 'Emmanuel', 'Heakey', '2 Towne Pass', 'Nagpandayan', 'Philippines', '61786-018', '6202223581', 'eheakey1j@hud.gov'),
  (57, 'Malcolm', 'Larwell', '7 Tennessee Way', 'Rayong', 'Thailand', '43063-426', '7094941117', 'mlarwell1k@homestead.com'),
  (58, 'Kelley', 'Heaviside', '58809 Meadow Ridge Circle', 'Saint Louis', 'United States', '0007-4896', '3144322726', 'kheaviside1l@cornell.edu'),
  (59, 'Hannah', 'Gurnett', '30 Nelson Way', 'Huangjin', 'China', '21695-041', '8276456946', 'hgurnett1m@paginegialle.it'),
  (60, 'Ferdy', 'Berrecloth', '0 Spaight Drive', 'Monte Branco', 'Portugal', '55316-154', '3879288887', 'fberrecloth1n@sohu.com'),
  (61, 'Dodie', 'Flaxon', '477 Claremont Court', 'Xufeng', 'China', '59667-0057', '3421940353', 'dflaxon1o@reverbnation.com'),
  (62, 'Lynnette', 'De Vuyst', '950 Marcy Alley', 'Zhankhoteko', 'Russia', '65373-401', '6064989250', 'ldevuyst1p@spiegel.de'),
  (63, 'Shelia', 'MacFadyen', '85 Corscot Terrace', 'Ganting', 'China', '49348-737', '4462326487', 'smacfadyen1q@tripadvisor.com'),
  (64, 'Rozanna', 'Bickerstasse', '1083 Sachs Center', 'Oropesa', 'Peru', '55154-5537', '2668139752', 'rbickerstasse1r@chron.com'),
  (65, 'Rutter', 'Shivlin', '93 Morrow Pass', 'At Tafilah', 'Jordan', '0615-2595', '8166024375', 'rshivlin1s@trellian.com'),
  (66, 'Emmit', 'Cullingford', '951 Sycamore Alley', 'Sendai-shi', 'Japan', '65044-0720', '6731235306', 'ecullingford1t@mashable.com'),
  (67, 'Elisha', 'O''Sheils', '17098 Hooker Junction', 'Cungking', 'Indonesia', '51346-117', '2177744102', 'eosheils1u@hhs.gov'),
  (68, 'Howey', 'Uwins', '45841 Superior Junction', 'Daohe', 'China', '68016-283', '2583505477', 'huwins1v@imgur.com'),
  (69, 'Sabine', 'Coil', '213 Valley Edge Court', 'Nueva Esperanza', 'Mexico', '0955-1032', '3256235136', 'scoil1w@telegraph.co.uk'),
  (70, 'Juli', 'Mealand', '6971 Almo Place', 'Dzüyl', 'Mongolia', '53645-1110', '6593272361', 'jmealand1x@i2i.jp'),
  (71, 'Inez', 'Fedorchenko', '0292 Dorton Crossing', 'Pristina', 'Kosovo', '0363-0462', '6232600987', 'ifedorchenko1y@scientificamerican.com'),
  (72, 'Alfons', 'Coatman', '262 Del Mar Lane', 'Besuki', 'Indonesia', '49348-955', '4322263628', 'acoatman1z@blinklist.com'),
  (73, 'Genvieve', 'Heinsius', '75100 Eagle Crest Court', 'Varkaus', 'Finland', '0069-2589', '2298457681', 'gheinsius20@reddit.com'),
  (74, 'Caritta', 'Rainsbury', '2565 Old Shore Pass', 'Dzhalilabad', 'Azerbaijan', '16590-259', '3917386266', 'crainsbury21@cargocollective.com'),
  (75, 'Cicily', 'Deeny', '769 Brentwood Street', 'Taiping', 'China', '21695-750', '3465381473', 'cdeeny22@barnesandnoble.com'),
  (76, 'Binni', 'Yekel', '4198 Drewry Parkway', 'Ouadda', 'Central African Republic', '68196-324', '8341835202', 'byekel23@cbslocal.com'),
  (77, 'Philis', 'Dymidowicz', '6 Fordem Avenue', 'San Antonio', 'Mexico', '49349-521', '6716051802', 'pdymidowicz24@si.edu'),
  (78, 'Augustine', 'Woolgar', '6 Fair Oaks Hill', 'Bunder', 'Indonesia', '0409-4712', '8315774220', 'awoolgar25@1und1.de'),
  (79, 'Danie', 'Kalisch', '71 Veith Junction', 'Porta', 'Portugal', '63629-2946', '3978125952', 'dkalisch26@deviantart.com'),
  (80, 'Edith', 'Dwelly', '89502 Miller Trail', 'Ranot', 'Thailand', '0603-2435', '1626751252', 'edwelly27@netlog.com'),
  (81, 'Dallas', 'Ivan', '9688 Forest Place', 'Ágios Andréas', 'Greece', '53808-0692', '2413678891', 'divan1@nature.com'),
  (82, 'Wakefield', 'Garlant', '665 South Center', 'Keyinhe', 'China', '68180-222', '8131232918', 'wgarlant2@tinyurl.com'),
  (83, 'Tarrance', 'Mattussevich', '7 Lindbergh Court', 'Shimoda', 'Japan', '65044-9954', '7369001398', 'tmattussevich3@yolasite.com'),
  (84, 'Roz', 'Godridge', '1 6th Junction', 'Los Angeles', 'United States', '31645-165', '3231703328', 'rgodridge4@miitbeian.gov.cn'),
  (85, 'Dosi', 'Beese', '3 Algoma Road', 'München', 'Germany', '42126-5200', '3219401714', 'dbeese5@eepurl.com'),
  (86, 'Nata', 'Giorgiutti', '55167 Logan Street', 'Yemva', 'Russia', '0280-6050', '6651812919', 'ngiorgiutti6@topsy.com'),
  (87, 'Nappie', 'Attwell', '387 Cordelia Way', 'Dikwa', 'Nigeria', '50523-100', '1857985800', 'nattwell7@cyberchimps.com'),
  (88, 'Danya', 'Nann', '07356 Hanover Circle', 'Dongjie', 'China', '68604-023', '8527169573', 'dnann8@opera.com'),
  (89, 'Morten', 'Lacase', '1 Saint Paul Avenue', 'Proptisht', 'Albania', '36987-2983', '5013369010', 'mlacase9@youtube.com'),
  (90, 'Tobe', 'Merioth', '2 Main Plaza', 'Bordeaux', 'France', '55154-3624', '6355635323', 'tmeriotha@wired.com'),
  (91, 'Abbot', 'Whitman', '5993 Schmedeman Center', 'Herceg-Novi', 'Montenegro', '50865-056', '9997055094', 'awhitmanb@google.ca'),
  (92, 'Jared', 'Stimson', '83 Nancy Way', 'Nykvarn', 'Sweden', '16252-509', '4153990754', 'jstimsonc@dailymail.co.uk'),
  (93, 'Blancha', 'Hay', '04 Florence Alley', 'Stupino', 'Russia', '60505-0015', '1011056686', 'bhayd@google.it'),
  (94, 'Hymie', 'Merida', '49753 Hermina Pass', 'Chaparral', 'Colombia', '59779-119', '7432517339', 'hmeridae@google.it'),
  (95, 'Englebert', 'Ginglell', '78 Blaine Avenue', 'Changqing', 'China', '68084-280', '7713472340', 'eginglellf@hexun.com'),
  (96, 'Iver', 'Petruska', '9034 Longview Road', 'Jatiwangi', 'Indonesia', '55312-759', '7602821123', 'ipetruskag@ed.gov'),
  (97, 'Devon', 'Stockney', '608 Rockefeller Court', 'Huangbei', 'China', '10237-745', '2297959695', 'dstockneyh@flavors.me'),
  (98, 'Ethelbert', 'Wadeson', '88485 Westport Street', 'Xianyuan', 'China', '0009-0073', '5669617445', 'ewadesoni@sohu.com'),
  (99, 'Rafael', 'Hillett', '9925 Rigney Parkway', 'Kaset Wisai', 'Thailand', '11822-0619', '1664195590', 'rhillettj@chronoengine.com'),
  (100, 'Roseline', 'Boobier', '198 Express Terrace', 'Fryazino', 'Russia', '62296-0032', '2248426731', 'rboobierk@slideshare.net')
ON CONFLICT ("ClientId") DO NOTHING;

INSERT INTO "Crews" ("CrewId", "CommanderId", "CopiloteId", "FachiefId", "FliAttendant1Id", "FliAttendant2Id", "FliAttendant3Id") VALUES
  (1, 3, 4, 5, 7, 8, 9),
  (2, 14, 12, 10, 11, 24, 25),
  (3, 18, 20, 26, 31, 32, 33),
  (4, 34, 35, 36, 37, 38, 39)
ON CONFLICT ("CrewId") DO NOTHING;

INSERT INTO "Shifts" ("ShiftId", "ShiftDate", "BeginTime", "EndTime", "CrewId") VALUES
  (1, '2022-09-01', '10:00:00', '15:00:00', 1),
  (2, '2022-09-01', '10:00:00', '15:00:00', 2),
  (3, '2022-09-01', '10:00:00', '15:00:00', 3),
  (4, '2022-09-01', '10:00:00', '15:00:00', 4)
ON CONFLICT ("ShiftId") DO NOTHING;

INSERT INTO "Flights" ("FlightId", "FlightDate", "DepTime", "ArrTime", "TotPass", "TotBagga", "FlightNum", "ShiftId", "AirplaneId", "AirportDepId", "AirportArrId") VALUES
  (1, '2022-09-01', '10:00:00', '15:00:00', 200, 15, 'CB2204', 1, 2, 1, 3),
  (2, '2022-09-01', '17:00:00', '20:00:00', 200, 15, 'CB2205', 1, 2, 3, 1),
  (3, '2022-09-01', '10:00:00', '15:00:00', 200, 15, 'CB1104', 2, 3, 1, 4),
  (4, '2022-09-01', '17:00:00', '20:00:00', 200, 15, 'CB1105', 2, 3, 4, 1),
  (5, '2022-09-01', '10:00:00', '15:00:00', 200, 15, 'CB3304', 3, 1, 1, 6),
  (6, '2022-09-01', '17:00:00', '20:00:00', 200, 15, 'CB3305', 3, 1, 6, 1),
  (7, '2022-09-01', '10:00:00', '15:00:00', 200, 15, 'CB4404', 4, 4, 1, 5),
  (8, '2022-09-01', '17:00:00', '20:00:00', 200, 15, 'CB4405', 4, 4, 5, 1)
ON CONFLICT ("FlightId") DO NOTHING;

INSERT INTO "Buys" ("BuyId", "BuyDate", "BuyTime", "Price", "EmpId", "ClientId") VALUES
  (1, '2022-09-08', '10:03:00', 100.00, 6, 6)
ON CONFLICT ("BuyId") DO NOTHING;

INSERT INTO "Tickets" ("TicketId", "BuyId", "ClientId", "FlightId", "Seat") VALUES
  (1, 1, 6, 4, 'B04')
ON CONFLICT ("TicketId") DO NOTHING;

COMMIT;