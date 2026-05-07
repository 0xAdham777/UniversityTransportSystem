USE UniversityTransportDB;
GO

-- =============================================
-- 1. WILAYA (All 58 Algerian Wilayas)
-- =============================================
INSERT INTO Wilaya (WilayaName, WilayaCode) VALUES
('Adrar', '01'),
('Chlef', '02'),
('Laghouat', '03'),
('Oum El Bouaghi', '04'),
('Batna', '05'),
('Béjaïa', '06'),
('Biskra', '07'),
('Béchar', '08'),
('Blida', '09'),
('Bouira', '10'),
('Tamanrasset', '11'),
('Tébessa', '12'),
('Tlemcen', '13'),
('Tiaret', '14'),
('Tizi Ouzou', '15'),
('Alger', '16'),
('Djelfa', '17'),
('Jijel', '18'),
('Sétif', '19'),
('Saïda', '20'),
('Skikda', '21'),
('Sidi Bel Abbès', '22'),
('Annaba', '23'),
('Guelma', '24'),
('Constantine', '25'),
('Médéa', '26'),
('Mostaganem', '27'),
('M''Sila', '28'),
('Mascara', '29'),
('Ouargla', '30'),
('Oran', '31'),
('El Bayadh', '32'),
('Illizi', '33'),
('Bordj Bou Arreridj', '34'),
('Boumerdès', '35'),
('El Tarf', '36'),
('Tindouf', '37'),
('Tissemsilt', '38'),
('El Oued', '39'),
('Khenchela', '40'),
('Souk Ahras', '41'),
('Tipaza', '42'),
('Mila', '43'),
('Aïn Defla', '44'),
('Naâma', '45'),
('Aïn Témouchent', '46'),
('Ghardaïa', '47'),
('Relizane', '48'),
('Timimoun', '49'),
('Bordj Badji Mokhtar', '50'),
('Ouled Djellal', '51'),
('Béni Abbès', '52'),
('In Salah', '53'),
('In Guezzam', '54'),
('Touggourt', '55'),
('Djanet', '56'),
('El MGhair', '57'),
('El Meniaa', '58');
GO

-- =============================================
-- 2. SAMPLE MUNICIPALITIES (key cities per wilaya)
-- =============================================
INSERT INTO Municipality (WilayaID, MunicipalityName, PostalCode) VALUES
(16, 'Alger Centre', '16000'),
(16, 'Bab El Oued', '16005'),
(16, 'Hussein Dey', '16010'),
(16, 'Kouba', '16050'),
(16, 'Birkhadem', '16030'),
(31, 'Oran', '31000'),
(31, 'Es Senia', '31100'),
(31, 'Bir El Djir', '31020'),
(25, 'Constantine', '25000'),
(25, 'El Khroub', '25100'),
(25, 'Aïn Smara', '25200'),
(19, 'Sétif', '19000'),
(19, 'El Eulma', '19600'),
(6, 'Béjaïa', '06000'),
(6, 'Akbou', '06200'),
(23, 'Annaba', '23000'),
(23, 'El Hadjar', '23100'),
(9, 'Blida', '09000'),
(9, 'Boufarik', '09200'),
(1, 'Adrar', '01000'),
(7, 'Biskra', '07000'),
(13, 'Tlemcen', '13000'),
(15, 'Tizi Ouzou', '15000'),
(15, 'Draâ Ben Khedda', '15100'),
(31, 'Mers El Kebir', '31150'),
(26, 'Médéa', '26000'),
(28, 'M''Sila', '28000'),
(29, 'Mascara', '29000'),
(30, 'Ouargla', '30000'),
(35, 'Boumerdès', '35000'),
(35, 'Boudouaou', '35100'),
(42, 'Tipaza', '42000'),
(42, 'Cherchell', '42100'),
(24, 'Guelma', '24000'),
(21, 'Skikda', '21000'),
(22, 'Sidi Bel Abbès', '22000'),
(17, 'Djelfa', '17000'),
(5, 'Batna', '05000'),
(3, 'Laghouat', '03000'),
(11, 'Tamanrasset', '11000'),
(12, 'Tébessa', '12000'),
(14, 'Tiaret', '14000'),
(18, 'Jijel', '18000'),
(20, 'Saïda', '20000'),
(27, 'Mostaganem', '27000'),
(32, 'El Bayadh', '32000'),
(33, 'Illizi', '33000'),
(34, 'Bordj Bou Arreridj', '34000'),
(36, 'El Tarf', '36000'),
(37, 'Tindouf', '37000'),
(38, 'Tissemsilt', '38000'),
(39, 'El Oued', '39000'),
(40, 'Khenchela', '40000'),
(41, 'Souk Ahras', '41000'),
(43, 'Mila', '43000'),
(44, 'Aïn Defla', '44000'),
(45, 'Naâma', '45000'),
(46, 'Aïn Témouchent', '46000'),
(47, 'Ghardaïa', '47000'),
(48, 'Relizane', '48000');
GO

-- =============================================
-- 3. DEPARTMENTS
-- =============================================
INSERT INTO Department (DepartmentName) VALUES
('Informatique'),
('Génie Civil'),
('Génie Mécanique'),
('Génie Électrique'),
('Électronique'),
('Mathématiques'),
('Physique'),
('Chimie'),
('Biologie'),
('Sciences Économiques'),
('Droit'),
('Médecine'),
('Pharmacie'),
('Langues Étrangères'),
('Lettres Arabes'),
('Histoire'),
('Géographie'),
('Psychologie'),
('Sociologie'),
('Sciences Politiques'),
('Architecture'),
('Agronomie'),
('Gestion'),
('Comptabilité');
GO

-- =============================================
-- 4. SPECIALITIES
-- =============================================
INSERT INTO Speciality (DepartmentID, SpecialityName) VALUES
(1, 'Systèmes Informatiques'),
(1, 'Intelligence Artificielle'),
(1, 'Réseaux et Télécommunications'),
(1, 'Génie Logiciel'),
(1, 'Cybersécurité'),
(2, 'Structures et Matériaux'),
(2, 'Routes et Ouvrages d''Art'),
(2, 'Hydraulique'),
(3, 'Construction Mécanique'),
(3, 'Énergétique'),
(3, 'Fabrication Mécanique'),
(4, 'Automatisme'),
(4, 'Électrotechnique'),
(5, 'Télécommunications'),
(5, 'Systèmes Embarqués'),
(6, 'Mathématiques Fondamentales'),
(6, 'Mathématiques Appliquées'),
(6, 'Statistique et Analyse'),
(7, 'Physique des Matériaux'),
(7, 'Physique Théorique'),
(8, 'Chimie Organique'),
(8, 'Chimie Minérale'),
(9, 'Biochimie'),
(9, 'Microbiologie'),
(9, 'Écologie'),
(10, 'Monnaie et Finance'),
(10, 'Économie du Développement'),
(11, 'Droit des Affaires'),
(11, 'Droit International'),
(11, 'Droit Pénal'),
(12, 'Médecine Générale'),
(12, 'Chirurgie'),
(13, 'Pharmacie Clinique'),
(13, 'Pharmacie Industrielle'),
(14, 'Anglais'),
(14, 'Français'),
(15, 'Langue et Littérature Arabes'),
(16, 'Histoire Ancienne'),
(16, 'Histoire Contemporaine'),
(17, 'Géographie Urbaine'),
(17, 'Aménagement du Territoire'),
(18, 'Psychologie Clinique'),
(18, 'Psychologie du Travail'),
(22, 'Production Végétale'),
(22, 'Production Animale'),
(23, 'Finance d''Entreprise'),
(23, 'Marketing'),
(21, 'Conception Architecturale'),
(21, 'Urbanisme'),
(24, 'Comptabilité Générale'),
(24, 'Audit et Contrôle de Gestion');
GO

-- =============================================
-- 5. INCIDENT TYPES
-- =============================================
INSERT INTO IncidentType (IncidentTypeName) VALUES
('Accident de la route'),
('Panne mécanique'),
('Panne électrique'),
('Pneumatique crevé'),
('Retard au départ'),
('Retard à l''arrivée'),
('Surcharge de passagers'),
('Conflit entre passagers'),
('Incident médical'),
('Panne de carburant'),
('Porte bloquée'),
('Problème de climatisation'),
('Intempéries'),
('Manifestation / Blocus routier'),
('Détournement de ligne'),
('Incident de sécurité');
GO

-- =============================================
-- 6. BUS MODELS
-- =============================================
INSERT INTO BusModel (ModelName, ManufacturerName, DefaultCapacity) VALUES
('Urban 2000', 'Mercedes-Benz', 50),
('Sprinter 516', 'Mercedes-Benz', 25),
('Tourismo', 'Mercedes-Benz', 55),
('Marcopolo Paradiso', 'Volvo', 50),
('Caio Apache', 'Volkswagen', 45),
('Master 35', 'Renault', 35),
('Master 50', 'Renault', 50),
('Scoler', 'IVECO', 45),
('Daily 70C', 'IVECO', 30),
('Citybus 12m', 'MAN', 60),
('Lion''s City', 'MAN', 55),
('H100', 'Hyundai', 25),
('County', 'Hyundai', 35),
('King Long XMQ', 'King Long', 50),
('Grandbird', 'Yutong', 55);
GO

-- =============================================
-- 7. SAMPLE PERSONS
-- =============================================
INSERT INTO Person (FirstName, MidName, LastName, DateOfBirth, Gender, PhoneNumber, Email, Address) VALUES
('Ahmed', 'Ben', 'Khalifa', '1998-03-15', 1, '0550-00-00-01', 'ahmed.khalifa@univ.dz', '15 Rue Didouche Mourad, Alger'),
('Fatima', NULL, 'Zohra', '2000-07-22', 0, '0550-00-00-02', 'fatima.zohra@univ.dz', '42 Rue Larbi Ben M''hidi, Alger'),
('Mohamed', 'Ali', 'Seddik', '1999-01-10', 1, '0550-00-00-03', 'mohamed.seddik@univ.dz', '08 Boulevard Belouizdad, Alger'),
('Salima', 'Brahim', 'Mansouri', '2001-11-05', 0, '0550-00-00-04', 'salima.mansouri@univ.dz', '21 Rue des Frères Arbaoui, Bab El Oued'),
('Karim', NULL, 'Ouali', '1997-06-18', 1, '0550-00-00-05', 'karim.ouali@univ.dz', '07 Cité Universitaire, Ben Aknoun'),
('Nassima', 'Said', 'Benyahia', '2002-02-28', 0, '0550-00-00-06', 'nassima.benyahia@univ.dz', '33 Rue Mohamed Belouizdad, Oran'),
('Reda', 'Assia', 'Toumi', '1996-09-14', 1, '0550-00-00-07', 'reda.toumi@univ.dz', '12 Avenue de l''ALN, Constantine'),
('Amira', NULL, 'Hadjadj', '2000-04-30', 0, '0550-00-00-08', 'amira.hadjadj@univ.dz', '56 Rue Aissat Idir, Sétif'),
('Lyes', 'Mohand', 'Ait Ouali', '1998-08-12', 1, '0550-00-00-09', 'lyes.aitouali@univ.dz', '18 Rue Frères Zaghloul, Bejaïa'),
('Ines', NULL, 'Boumediene', '2001-12-03', 0, '0550-00-00-10', 'ines.boumediene@univ.dz', '09 Cité 5 Juillet, Annaba'),
('Samir', 'Noureddine', 'Kaci', '1995-05-20', 1, '0550-00-00-11', 'samir.kaci@univ.dz', '22 Rue des Remparts, Tlemcen'),
('Yasmine', 'Tahar', 'Zerrouki', '1999-10-08', 0, '0550-00-00-12', 'yasmine.zerrouki@univ.dz', '14 Boulevard de l''Indépendance, Blida'),
('Walid', NULL, 'Bensaid', '1997-03-25', 1, '0550-00-00-13', 'walid.bensaid@univ.dz', '28 Rue des Martyrs, Tizi Ouzou'),
('Imane', 'Hamid', 'Merabet', '2003-07-14', 0, '0550-00-00-14', 'imane.merabet@univ.dz', '03 Cité Emir Abdelkader, Skikda'),
('Tarek', NULL, 'Bouaziz', '1998-11-30', 1, '0550-00-00-15', 'tarek.bouaziz@univ.dz', '17 Rue Ahmed Boughani, Djelfa'),
('Kenza', 'Rabah', 'Saidi', '2000-08-22', 0, '0550-00-00-16', 'kenza.saidi@univ.dz', '06 Cité 1er Novembre, Guelma'),
('Zakaria', NULL, 'Ghalem', '1996-04-17', 1, '0550-00-00-17', 'zakaria.ghalem@univ.dz', '11 Rue des Frères Amrani, Mila'),
('Chaima', 'Yacine', 'Djemai', '2001-09-05', 0, '0550-00-00-18', 'chaima.djemai@univ.dz', '32 Cité des Oliviers, Bouira'),
('Hicham', NULL, 'Nekkache', '1999-12-28', 1, '0550-00-00-19', 'hicham.nekkache@univ.dz', '44 Rue Hassiba Ben Bouali, Médéa'),
('Sofia', 'Abdelkader', 'Gherairi', '2002-06-11', 0, '0550-00-00-20', 'sofia.gherairi@univ.dz', '27 Rue Mohamed Khider, Batna');
GO

-- =============================================
-- 8. STUDENTS
-- =============================================
INSERT INTO Student (PersonID, SpecialityID, StudentStatus) VALUES
(1, 1, 1), (2, 5, 1), (3, 3, 1), (4, 2, 1), (5, 4, 1),
(6, 9, 1), (7, 7, 1), (8, 11, 1), (9, 14, 1), (10, 10, 1),
(11, 13, 1), (12, 6, 1), (13, 15, 1), (14, 8, 1), (15, 12, 1),
(16, 17, 1), (17, 19, 1), (18, 20, 1), (19, 23, 1), (20, 4, 1);
GO

-- =============================================
-- 9. EMPLOYEES (Drivers + Staff)
-- =============================================
INSERT INTO Person (FirstName, MidName, LastName, DateOfBirth, Gender, PhoneNumber, Email, Address) VALUES
('Said', NULL, 'Boumediene', '1975-09-12', 1, '0660-11-22-01', 'said.boumediene@transport.dz', '05 Rue Colonel Lotfi, Alger'),
('Ali', NULL, 'Touati', '1982-04-25', 1, '0660-11-22-02', 'ali.touati@transport.dz', '19 Rue des Dunes, Oran'),
('Messaoud', 'Amar', 'Belkacem', '1978-11-08', 1, '0660-11-22-03', 'messaoud.belkacem@transport.dz', '36 Cité des Asphodèles, Constantine'),
('Rachid', 'Omar', 'Hadj', '1985-07-30', 1, '0660-11-22-04', 'rachid.hadj@transport.dz', '02 Rue Abane Ramdane, Blida'),
('Farida', NULL, 'Mekki', '1990-02-14', 0, '0660-11-22-05', 'farida.mekki@transport.dz', '48 Rue des Frères Abbès, Alger'),
('Kamel', NULL, 'Dahmani', '1980-12-01', 1, '0660-11-22-06', 'kamel.dahmani@transport.dz', '07 Cité Belle Vue, Sétif'),
('Nacer', 'Amokrane', 'Ouzellaguen', '1976-08-18', 1, '0660-11-22-07', 'nacer.ouzellaguen@transport.dz', '25 Rue de la Liberté, Bejaïa'),
('Zineb', NULL, 'Chikhi', '1992-03-22', 0, '0660-11-22-08', 'zineb.chikhi@transport.dz', '13 Cité des Roses, Tizi Ouzou');
GO

INSERT INTO Employee (PersonID, HireDate, EmployeeStatus) VALUES
(21, '2010-05-01', 1),
(22, '2012-09-15', 1),
(23, '2008-03-20', 1),
(24, '2015-11-01', 1),
(25, '2018-06-10', 1),
(26, '2014-02-25', 1),
(27, '2009-07-14', 1),
(28, '2021-01-05', 1);
GO

INSERT INTO Driver (EmployeeID, LicenseNumber, LicenseExpiryDate, DriverStatus) VALUES
(1, 'DZ-198376-A', '2027-05-12', 1),
(2, 'DZ-198377-B', '2026-11-25', 1),
(3, 'DZ-198378-C', '2028-03-08', 1),
(4, 'DZ-198379-A', '2027-07-30', 1),
(6, 'DZ-198380-B', '2026-09-15', 1),
(7, 'DZ-198381-C', '2028-01-20', 1);
GO

-- =============================================
-- 10. STATIONS
-- =============================================
INSERT INTO Station (StationName, LocationDescription, MunicipalityID, StationStatus) VALUES
('Université Alger 1 - Ben Aknoun', 'Campus central Ben Aknoun', 4, 1),
('Université Alger 2 - Bouzaréah', 'Campus Bouzaréah', 1, 1),
('Université USTHB - Bab Ezzouar', 'Campus USTHB Bab Ezzouar', 2, 1),
('Cité Universitaire - Ben Aknoun', 'Résidence universitaire Ben Aknoun', 4, 1),
('Place 1er Mai', 'Station centrale Place 1er Mai', 1, 1),
('Gare Routière - Alger', 'Gare routière de Bab Ezzouar', 2, 1),
('Université d''Oran - Es Senia', 'Campus Es Senia', 7, 1),
('Cité Universitaire - Oran', 'Résidence universitaire Ouest', 8, 1),
('Université Constantine 1', 'Campus Chaab Erssas', 10, 1),
('Cité Universitaire - Constantine', 'Résidence 500 logements', 10, 1),
('Université Sétif 1', 'Campus El Bez', 12, 1),
('Cité Universitaire - Sétif', 'Résidence universitaire', 12, 1),
('Université de Béjaïa', 'Campus Targa Ouzemour', 13, 1),
('Gare Routière - Béjaïa', 'Station de transport Béjaïa', 13, 1),
('Université Annaba', 'Campus El Hadjar', 15, 1),
('Cité Universitaire - Annaba', 'Résidence Sidi Salem', 15, 1),
('Université de Blida 1', 'Campus Ben Achour', 17, 1),
('Université Tizi Ouzou', 'Campus Hasnaoua', 19, 1),
('Cité Universitaire - Tizi Ouzou', 'Résidence Tamda', 19, 1),
('Université de Tlemcen', 'Campus Chetouane', 18, 1),
('Gare Centrale - Alger', 'Place des Martyrs', 1, 1),
('Université de Skikda', 'Campus El Hadaiek', 26, 1),
('Université de Laghouat', 'Campus Ghardaïa', 27, 1),
('Université de Biskra', 'Campus Sidi Okba', 20, 1);
GO

-- =============================================
-- 11. TRANSPORT LINES
-- =============================================
INSERT INTO TransportLine (LineName, OriginStationID, DestinationStationID, LineStatus) VALUES
('L1 - Alger Centre ↔ USTHB', 5, 3, 1),
('L2 - Ben Aknoun ↔ Cité Universitaire', 1, 4, 1),
('L3 - Place 1er Mai ↔ Université Alger 2', 5, 2, 1),
('L4 - Gare Routière ↔ USTHB', 6, 3, 1),
('L5 - Oran Es Senia ↔ Cité Universitaire', 7, 8, 1),
('L6 - Constantine ↔ Cité Universitaire', 9, 10, 1),
('L7 - Sétif Université ↔ Cité', 11, 12, 1),
('L8 - Béjaïa Université ↔ Gare', 13, 14, 1),
('L9 - Annaba Université ↔ Cité', 15, 16, 1),
('L10 - Blida Université ↔ Centre', 17, 5, 1),
('L11 - Tizi Ouzou ↔ Cité Tamda', 18, 19, 1),
('L12 - Alger Centre ↔ Ben Aknoun', 21, 1, 1);
GO

-- =============================================
-- 12. LINE STATIONS
-- =============================================
INSERT INTO LineStation (TransportLineID, StationID, StationOrder, DistanceFromOrigin) VALUES
-- L1: Alger Centre → USTHB
(1, 5, 1, 0.0), (1, 21, 2, 1.5), (1, 1, 3, 4.2), (1, 3, 4, 8.0),
-- L2: Ben Aknoun ↔ Cité U
(2, 1, 1, 0.0), (2, 4, 2, 2.5),
-- L3: Place 1er Mai → Univ Alger 2
(3, 5, 1, 0.0), (3, 21, 2, 1.5), (3, 2, 3, 6.5),
-- L4: Gare Routière → USTHB
(4, 6, 1, 0.0), (4, 3, 2, 3.0),
-- L5: Oran Es Senia → Cité U
(5, 7, 1, 0.0), (5, 8, 2, 5.5),
-- L6: Constantine → Cité U
(6, 9, 1, 0.0), (6, 10, 2, 4.0),
-- L7: Sétif Univ → Cité
(7, 11, 1, 0.0), (7, 12, 2, 3.2),
-- L8: Béjaïa Univ → Gare
(8, 13, 1, 0.0), (8, 14, 2, 6.0),
-- L9: Annaba Univ → Cité
(9, 15, 1, 0.0), (9, 16, 2, 4.5),
-- L10: Blida Univ → Centre
(10, 17, 1, 0.0), (10, 5, 2, 8.5),
-- L11: Tizi Ouzou → Cité Tamda
(11, 18, 1, 0.0), (11, 19, 2, 3.8),
-- L12: Alger Centre → Ben Aknoun
(12, 21, 1, 0.0), (12, 1, 2, 4.2);
GO

-- =============================================
-- 13. SCHEDULES
-- =============================================
INSERT INTO Schedule (TransportLineID, DayOfWeek, DepartureTime, ArrivalTime, ScheduleStatus) VALUES
-- L1
(1, 'Samedi', '07:00', '07:45', 1),
(1, 'Samedi', '08:00', '08:45', 1),
(1, 'Samedi', '12:00', '12:45', 1),
(1, 'Samedi', '17:00', '17:45', 1),
(1, 'Dimanche', '07:00', '07:45', 1),
(1, 'Dimanche', '12:00', '12:45', 1),
(1, 'Lundi', '07:00', '07:45', 1),
(1, 'Lundi', '08:00', '08:45', 1),
(1, 'Lundi', '12:00', '12:45', 1),
(1, 'Lundi', '17:00', '17:45', 1),
(1, 'Mardi', '07:00', '07:45', 1),
(1, 'Mardi', '12:00', '12:45', 1),
(1, 'Mercredi', '07:00', '07:45', 1),
(1, 'Mercredi', '12:00', '12:45', 1),
(1, 'Jeudi', '07:00', '07:45', 1),
(1, 'Jeudi', '12:00', '12:45', 1),
-- L2
(2, 'Samedi', '07:30', '07:50', 1),
(2, 'Samedi', '13:00', '13:20', 1),
(2, 'Dimanche', '07:30', '07:50', 1),
(2, 'Lundi', '07:30', '07:50', 1),
(2, 'Lundi', '13:00', '13:20', 1),
(2, 'Mardi', '07:30', '07:50', 1),
(2, 'Mercredi', '07:30', '07:50', 1),
(2, 'Jeudi', '07:30', '07:50', 1),
-- L3
(3, 'Samedi', '06:45', '07:30', 1),
(3, 'Samedi', '11:30', '12:15', 1),
(3, 'Lundi', '06:45', '07:30', 1),
(3, 'Lundi', '11:30', '12:15', 1),
(3, 'Mercredi', '06:45', '07:30', 1),
(3, 'Mercredi', '11:30', '12:15', 1),
-- L5
(5, 'Samedi', '07:15', '07:50', 1),
(5, 'Samedi', '12:30', '13:05', 1),
(5, 'Dimanche', '07:15', '07:50', 1),
(5, 'Lundi', '07:15', '07:50', 1),
(5, 'Lundi', '12:30', '13:05', 1),
(5, 'Mardi', '07:15', '07:50', 1),
(5, 'Mercredi', '07:15', '07:50', 1),
(5, 'Jeudi', '07:15', '07:50', 1),
-- L6
(6, 'Samedi', '07:00', '07:35', 1),
(6, 'Samedi', '12:00', '12:35', 1),
(6, 'Lundi', '07:00', '07:35', 1),
(6, 'Lundi', '12:00', '12:35', 1),
(6, 'Mercredi', '07:00', '07:35', 1),
-- L12
(12, 'Samedi', '07:00', '07:20', 1),
(12, 'Samedi', '08:00', '08:20', 1),
(12, 'Samedi', '12:00', '12:20', 1),
(12, 'Samedi', '17:00', '17:20', 1),
(12, 'Dimanche', '07:00', '07:20', 1),
(12, 'Lundi', '07:00', '07:20', 1),
(12, 'Lundi', '12:00', '12:20', 1),
(12, 'Mardi', '07:00', '07:20', 1),
(12, 'Mercredi', '07:00', '07:20', 1),
(12, 'Jeudi', '07:00', '07:20', 1);
GO

-- =============================================
-- 14. BUSES
-- =============================================
INSERT INTO Bus (BusModelID, PlateNumber, BusCode, ManufacturingYear, BusStatus) VALUES
(1, '168-345-01', 'BUS-ALG-001', 2020, 1),
(1, '168-345-02', 'BUS-ALG-002', 2020, 1),
(3, '168-345-03', 'BUS-ALG-003', 2021, 1),
(2, '168-345-04', 'BUS-ALG-004', 2022, 1),
(1, '168-345-05', 'BUS-ALG-005', 2020, 1),
(5, '168-345-06', 'BUS-ALG-006', 2023, 1),
(4, '310-123-01', 'BUS-ORN-001', 2021, 1),
(4, '310-123-02', 'BUS-ORN-002', 2021, 1),
(7, '250-456-01', 'BUS-CON-001', 2019, 1),
(7, '250-456-02', 'BUS-CON-002', 2019, 1),
(6, '190-789-01', 'BUS-SET-001', 2022, 1),
(1, '060-111-01', 'BUS-BJA-001', 2020, 1),
(3, '230-222-01', 'BUS-ANN-001', 2021, 1),
(2, '090-333-01', 'BUS-BLD-001', 2022, 1),
(10, '150-444-01', 'BUS-TIZ-001', 2023, 1),
(8, '130-555-01', 'BUS-TLM-001', 2020, 1),
(1, '160-666-01', 'BUS-ALG-007', 2021, 1),
(5, '310-777-01', 'BUS-ORN-003', 2023, 1),
(4, '250-888-01', 'BUS-CON-003', 2022, 1),
(10, '160-999-01', 'BUS-ALG-008', 2023, 1);
GO

-- =============================================
-- 15. BUS ASSIGNMENTS
-- =============================================
INSERT INTO BusAssignment (BusID, TransportLineID, StartDate, EndDate, AssignmentStatus) VALUES
(1, 1, '2025-09-01', NULL, 1),
(2, 1, '2025-09-01', NULL, 1),
(3, 12, '2025-09-01', NULL, 1),
(4, 2, '2025-09-01', NULL, 1),
(5, 3, '2025-09-01', NULL, 1),
(6, 4, '2025-10-01', NULL, 1),
(7, 5, '2025-09-01', NULL, 1),
(8, 5, '2025-09-01', NULL, 1),
(9, 6, '2025-09-01', NULL, 1),
(10, 6, '2025-09-01', NULL, 1),
(11, 7, '2025-09-01', NULL, 1),
(12, 8, '2025-09-01', NULL, 1),
(13, 9, '2025-09-01', NULL, 1),
(14, 10, '2025-09-01', NULL, 1),
(15, 11, '2025-10-01', NULL, 1),
(16, 1, '2025-10-15', NULL, 1),
(17, 3, '2025-09-15', NULL, 1),
(18, 5, '2025-10-01', NULL, 1),
(19, 6, '2025-10-01', NULL, 1),
(20, 12, '2025-11-01', NULL, 1);
GO

-- =============================================
-- 16. TRANSPORT SUBSCRIPTIONS
-- =============================================
INSERT INTO TransportSubscription (StudentID, TransportLineID, StartDate, EndDate, SubscriptionStatus) VALUES
(1, 1, '2025-10-01', '2026-06-30', 1),
(2, 12, '2025-10-01', '2026-06-30', 1),
(3, 1, '2025-10-01', '2026-06-30', 1),
(4, 2, '2025-10-01', '2026-06-30', 1),
(5, 3, '2025-10-01', '2026-06-30', 1),
(6, 5, '2025-10-01', '2026-06-30', 1),
(7, 6, '2025-10-01', '2026-06-30', 1),
(8, 7, '2025-10-01', '2026-06-30', 1),
(9, 8, '2025-10-01', '2026-06-30', 1),
(10, 5, '2025-10-01', '2026-06-30', 1),
(11, 7, '2025-10-01', '2026-06-30', 1),
(12, 6, '2025-10-01', '2026-06-30', 1),
(13, 11, '2025-10-01', '2026-06-30', 1),
(14, 5, '2025-10-01', '2026-06-30', 1),
(15, 12, '2025-10-01', '2026-06-30', 1),
(16, 1, '2025-10-01', '2026-06-30', 1),
(17, 6, '2025-10-01', '2026-06-30', 1),
(18, 1, '2025-10-01', '2026-06-30', 1),
(19, 10, '2025-10-01', '2026-06-30', 1),
(20, 1, '2025-11-01', '2026-06-30', 1);
GO

-- =============================================
-- 17. SUBSCRIPTION PAYMENTS
-- =============================================
INSERT INTO SubscriptionPayment (TransportSubscriptionID, Amount, PaymentDate, PaymentStatus) VALUES
(1, 5000.00, '2025-10-05', 1),
(2, 5000.00, '2025-10-03', 1),
(3, 5000.00, '2025-10-05', 1),
(4, 5000.00, '2025-10-02', 1),
(5, 5000.00, '2025-10-06', 1),
(6, 5000.00, '2025-10-07', 1),
(7, 5000.00, '2025-10-04', 1),
(8, 5000.00, '2025-10-05', 1),
(9, 5000.00, '2025-10-08', 1),
(10, 5000.00, '2025-10-03', 1),
(11, 5000.00, '2025-10-09', 1),
(12, 5000.00, '2025-10-04', 1),
(13, 5000.00, '2025-10-07', 1),
(14, 5000.00, '2025-10-05', 1),
(15, 5000.00, '2025-10-06', 1),
(16, 5000.00, '2025-10-08', 1),
(17, 5000.00, '2025-10-10', 1),
(18, 5000.00, '2025-10-07', 1),
(19, 5000.00, '2025-10-02', 1),
(20, 5000.00, '2025-11-02', 1);
GO

-- =============================================
-- 18. TRIPS (sample completed trips)
-- =============================================
INSERT INTO Trip (BusID, DriverID, TransportLineID, ScheduleID, TripDate, ActualDepartureTime, ActualArrivalTime, TripStatus, DelayInMinutes) VALUES
(1, 1, 1, 1, '2026-05-01', '07:02', '07:48', 1, 3),
(1, 1, 1, 3, '2026-05-01', '12:05', '12:50', 1, 5),
(2, 2, 1, 2, '2026-05-01', '08:00', '08:45', 1, 0),
(3, 3, 12, 46, '2026-05-01', '07:00', '07:22', 1, 2),
(4, 4, 2, 17, '2026-05-01', '07:32', '07:53', 1, 2),
(5, 5, 3, 25, '2026-05-01', '06:48', '07:33', 1, 3),
(7, 6, 5, 31, '2026-05-01', '07:18', '07:55', 1, 3),
(1, 1, 1, 1, '2026-05-02', '07:00', '07:44', 1, 0),
(2, 2, 1, 2, '2026-05-02', '08:02', '08:47', 1, 2),
(3, 3, 12, 46, '2026-05-02', '07:01', '07:20', 1, 1),
(9, 5, 6, 39, '2026-05-02', '07:00', '07:35', 1, 0),
(5, 4, 3, 26, '2026-05-02', '11:35', '12:18', 1, 5),
(3, 3, 12, 47, '2026-05-02', '08:00', '08:21', 1, 1),
(1, 1, 1, 3, '2026-05-03', '12:00', '12:47', 1, 0),
(2, 2, 1, 4, '2026-05-03', '17:05', '17:50', 1, 5),
(7, 6, 5, 32, '2026-05-03', '12:35', '13:10', 1, 5),
(4, 4, 2, 18, '2026-05-03', '13:02', '13:22', 1, 2),
(1, 1, 1, 1, '2026-05-04', '07:00', '07:45', 1, 0),
(9, 5, 6, 40, '2026-05-04', '12:00', '12:37', 1, 2),
(3, 3, 12, 48, '2026-05-04', '12:00', '12:22', 1, 2);
GO

-- =============================================
-- 19. STUDENT TRIP ATTENDANCE
-- =============================================
INSERT INTO StudentTripAttendance (StudentID, TripID, BoardingStationID, DropOffStationID, BoardingTime, DropOffTime, AttendanceStatus, Notes) VALUES
(1, 1, 1, 3, '07:02', '07:48', 1, NULL),
(2, 1, 21, 1, '07:05', '07:25', 1, NULL),
(3, 2, 4, 3, '12:05', '12:50', 1, NULL),
(4, 4, 1, 4, '07:32', '07:53', 1, NULL),
(5, 5, 21, 2, '06:48', '07:33', 1, NULL),
(1, 8, 1, 3, '07:00', '07:44', 1, NULL),
(2, 8, 21, 1, '07:02', '07:22', 1, NULL),
(3, 9, 1, 3, '08:02', '08:47', 1, NULL),
(15, 10, 21, 1, '07:01', '07:20', 1, NULL),
(18, 13, 1, 3, '08:00', '08:21', 1, NULL),
(1, 14, 1, 3, '12:00', '12:47', 1, NULL),
(2, 14, 21, 1, '12:03', '12:25', 1, NULL),
(15, 18, 21, 1, '07:00', '07:22', 1, NULL),
(1, 18, 1, 3, '07:00', '07:45', 1, NULL),
(4, 17, 1, 4, '13:02', '13:22', 1, NULL);
GO

-- =============================================
-- 20. INCIDENTS
-- =============================================
INSERT INTO Incident (TripID, ReportedByEmployeeID, IncidentTypeID, IncidentDescription, IncidentDateTime) VALUES
(1, 5, 6, 'Retard de 3 minutes dû à un embouteillage sur le boulevard', '2026-05-01 07:02:00'),
(5, 5, 6, 'Retard de 5 minutes, circulation dense', '2026-05-01 12:05:00'),
(12, 5, 6, 'Retard de 5 minutes, forte affluence à la station', '2026-05-02 11:35:00'),
(16, 1, 7, 'Surcharge de 5 passagers au départ, bus complet', '2026-05-03 12:35:00'),
(7, 1, 6, 'Retard de 3 minutes, attente des étudiants', '2026-05-01 07:18:00'),
(3, 2, 2, 'Panne mineure du moteur au démarrage, résolue en 10 min', '2026-05-01 08:00:00');
GO

PRINT '✅ Seed data inserted successfully!';
GO