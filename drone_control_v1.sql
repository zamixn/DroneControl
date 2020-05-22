-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 22, 2020 at 05:19 PM
-- Server version: 10.4.11-MariaDB
-- PHP Version: 7.4.5

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `drone_control_v1`
--

-- --------------------------------------------------------

--
-- Table structure for table `coordinate`
--

CREATE TABLE `coordinate` (
  `longitude` varchar(20) NOT NULL,
  `latitude` varchar(20) NOT NULL,
  `id` int(11) NOT NULL,
  `fk_route` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `coordinate`
--

INSERT INTO `coordinate` (`longitude`, `latitude`, `id`, `fk_route`) VALUES
('23.902732', '54.892874', 1, 1),
('25.902732', '54.892874', 2, 2),
('23.902732', '10.892874', 3, 1),
('6456', '6546', 4, 3),
('654645', '654645', 5, 3),
('645', '987', 6, 3),
('123213', '13123', 7, 4),
('21312', '21312', 8, 4),
('21321', '213123', 9, 4),
('5435', '453', 10, 4),
('23123', '3211', 22, 1009),
('345345', '323145', 23, 1010),
('123123', '3321', 24, 1011),
('112', '23232', 25, 1012),
('313313', '22211', 26, 1013),
('746534', '1234567', 27, 1014),
('3423', '7564', 28, 1015);

-- --------------------------------------------------------

--
-- Table structure for table `drone`
--

CREATE TABLE `drone` (
  `id` int(11) NOT NULL,
  `model` varchar(20) NOT NULL,
  `BatteryRemaining` decimal(10,0) NOT NULL,
  `State` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `drone`
--

INSERT INTO `drone` (`id`, `model`, `BatteryRemaining`, `State`) VALUES
(1, 'good alpha-1', '50', 1),
(2, 'gooder 3000', '100', 1),
(5, 'goodest T-1000', '100', 1),
(6, 'gooderer', '100', 1),
(7, '', '100', 1),
(8, 'asdfsdfasgadfgagfdgd', '100', 1),
(9, 'asd', '100', 1),
(10, 'fasgdwfg', '100', 1),
(11, 'fgh', '100', 1),
(12, 'testas test', '100', 2);

-- --------------------------------------------------------

--
-- Table structure for table `dronestate`
--

CREATE TABLE `dronestate` (
  `id` int(11) NOT NULL,
  `name` char(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `dronestate`
--

INSERT INTO `dronestate` (`id`, `name`) VALUES
(1, 'Off'),
(2, 'Charging'),
(3, 'OnWayLot'),
(4, 'Emergency'),
(5, 'EmergencyL'),
(6, 'OnWayBase'),
(7, 'ScanStart'),
(8, 'ScanScan'),
(9, 'ScanEnd');

-- --------------------------------------------------------

--
-- Table structure for table `fine`
--

CREATE TABLE `fine` (
  `id` int(11) NOT NULL,
  `Date` date NOT NULL,
  `Sum` decimal(10,0) NOT NULL,
  `State` int(11) NOT NULL,
  `fk_reservation` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `fine`
--

INSERT INTO `fine` (`id`, `Date`, `Sum`, `State`, `fk_reservation`) VALUES
(1, '2020-04-08', '50', 3, 1);

-- --------------------------------------------------------

--
-- Table structure for table `finestate`
--

CREATE TABLE `finestate` (
  `id` int(11) NOT NULL,
  `name` char(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `finestate`
--

INSERT INTO `finestate` (`id`, `name`) VALUES
(1, 'Formed'),
(2, 'NotPayed'),
(3, 'Payed'),
(4, 'PassedOn');

-- --------------------------------------------------------

--
-- Table structure for table `parkinglot`
--

CREATE TABLE `parkinglot` (
  `id` int(11) NOT NULL,
  `Address` text NOT NULL,
  `TotalSpaces` int(11) NOT NULL,
  `reservedSpaces` int(11) NOT NULL,
  `State` int(11) NOT NULL,
  `numberCheckTimeSpan` smallint(6) NOT NULL,
  `lastDroneVisit` datetime DEFAULT NULL,
  `fk_Drone` int(11) NOT NULL,
  `fk_RouteFrom` int(11) NOT NULL,
  `fk_RouteTo` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `parkinglot`
--

INSERT INTO `parkinglot` (`id`, `Address`, `TotalSpaces`, `reservedSpaces`, `State`, `numberCheckTimeSpan`, `lastDroneVisit`, `fk_Drone`, `fk_RouteFrom`, `fk_RouteTo`) VALUES
(1, 'Kaunas, LT Gatviausko g.', 59, 42, 1, 45, NULL, 1, 1, 2),
(10, 'Studentu g. 15', 64, 32, 2, 30, NULL, 2, 3, 4),
(35, 'sw all gatve', 95, 98, 1, 95, '2020-05-16 01:01:00', 10, 1014, 1015);

-- --------------------------------------------------------

--
-- Table structure for table `parkinglotstate`
--

CREATE TABLE `parkinglotstate` (
  `id` int(11) NOT NULL,
  `name` char(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `parkinglotstate`
--

INSERT INTO `parkinglotstate` (`id`, `name`) VALUES
(1, 'Open'),
(2, 'Closed');

-- --------------------------------------------------------

--
-- Table structure for table `reservation`
--

CREATE TABLE `reservation` (
  `LicensePlate` varchar(20) NOT NULL,
  `OwnerPhoneNumbers` varchar(20) NOT NULL,
  `ReservationDate` date NOT NULL,
  `ReservationDuration` time NOT NULL,
  `id` int(11) NOT NULL,
  `fk_parkingLot` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `reservation`
--

INSERT INTO `reservation` (`LicensePlate`, `OwnerPhoneNumbers`, `ReservationDate`, `ReservationDuration`, `id`, `fk_parkingLot`) VALUES
('123abc', '864269420', '2020-04-15', '22:00:00', 1, 1);

-- --------------------------------------------------------

--
-- Table structure for table `route`
--

CREATE TABLE `route` (
  `id` int(11) NOT NULL,
  `Height` decimal(10,0) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `route`
--

INSERT INTO `route` (`id`, `Height`) VALUES
(1, '0'),
(2, '5'),
(3, '0'),
(4, '25'),
(999, '999'),
(1000, '1000'),
(1009, '1233'),
(1010, '21113'),
(1011, '1222'),
(1012, '23332'),
(1013, '2111'),
(1014, '2312312'),
(1015, '123456');

-- --------------------------------------------------------

--
-- Table structure for table `worker`
--

CREATE TABLE `worker` (
  `id` int(11) NOT NULL,
  `Username` varchar(20) NOT NULL,
  `PasswordHash` varchar(20) NOT NULL,
  `WorkerRole` int(11) NOT NULL,
  `Name` varchar(20) NOT NULL,
  `Surname` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `worker`
--

INSERT INTO `worker` (`id`, `Username`, `PasswordHash`, `WorkerRole`, `Name`, `Surname`) VALUES
(1, 'admin', '123123123', 0, 'Admin', 'Admin'),
(2, 'peon', '123123123', 1, 'Peon', 'Peon');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `coordinate`
--
ALTER TABLE `coordinate`
  ADD PRIMARY KEY (`id`,`fk_route`),
  ADD KEY `fk_route` (`fk_route`);

--
-- Indexes for table `drone`
--
ALTER TABLE `drone`
  ADD PRIMARY KEY (`id`),
  ADD KEY `State` (`State`),
  ADD KEY `State_2` (`State`);

--
-- Indexes for table `dronestate`
--
ALTER TABLE `dronestate`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id` (`id`,`name`);

--
-- Indexes for table `fine`
--
ALTER TABLE `fine`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `fk_reservation` (`fk_reservation`),
  ADD KEY `State` (`State`);

--
-- Indexes for table `finestate`
--
ALTER TABLE `finestate`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id` (`id`,`name`);

--
-- Indexes for table `parkinglot`
--
ALTER TABLE `parkinglot`
  ADD PRIMARY KEY (`id`,`fk_RouteFrom`,`fk_RouteTo`),
  ADD UNIQUE KEY `fk_Drone` (`fk_Drone`),
  ADD UNIQUE KEY `fk_RouteFrom` (`fk_RouteFrom`),
  ADD UNIQUE KEY `fk_RouteTo` (`fk_RouteTo`),
  ADD KEY `State` (`State`);

--
-- Indexes for table `parkinglotstate`
--
ALTER TABLE `parkinglotstate`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id` (`id`,`name`);

--
-- Indexes for table `reservation`
--
ALTER TABLE `reservation`
  ADD PRIMARY KEY (`id`,`fk_parkingLot`),
  ADD KEY `fk_parkingLot` (`fk_parkingLot`);

--
-- Indexes for table `route`
--
ALTER TABLE `route`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `worker`
--
ALTER TABLE `worker`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `coordinate`
--
ALTER TABLE `coordinate`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- AUTO_INCREMENT for table `drone`
--
ALTER TABLE `drone`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `dronestate`
--
ALTER TABLE `dronestate`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `fine`
--
ALTER TABLE `fine`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `finestate`
--
ALTER TABLE `finestate`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `parkinglot`
--
ALTER TABLE `parkinglot`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=36;

--
-- AUTO_INCREMENT for table `parkinglotstate`
--
ALTER TABLE `parkinglotstate`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `reservation`
--
ALTER TABLE `reservation`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `route`
--
ALTER TABLE `route`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1016;

--
-- AUTO_INCREMENT for table `worker`
--
ALTER TABLE `worker`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `coordinate`
--
ALTER TABLE `coordinate`
  ADD CONSTRAINT `coordinate_ibfk_1` FOREIGN KEY (`fk_route`) REFERENCES `route` (`id`);

--
-- Constraints for table `drone`
--
ALTER TABLE `drone`
  ADD CONSTRAINT `drone_ibfk_1` FOREIGN KEY (`State`) REFERENCES `dronestate` (`id`);

--
-- Constraints for table `fine`
--
ALTER TABLE `fine`
  ADD CONSTRAINT `fine_ibfk_1` FOREIGN KEY (`State`) REFERENCES `finestate` (`id`),
  ADD CONSTRAINT `issued` FOREIGN KEY (`fk_reservation`) REFERENCES `reservation` (`id`);

--
-- Constraints for table `parkinglot`
--
ALTER TABLE `parkinglot`
  ADD CONSTRAINT `assigned` FOREIGN KEY (`fk_Drone`) REFERENCES `drone` (`id`),
  ADD CONSTRAINT `parkinglot_ibfk_1` FOREIGN KEY (`fk_RouteFrom`) REFERENCES `route` (`id`),
  ADD CONSTRAINT `parkinglot_ibfk_2` FOREIGN KEY (`fk_RouteTo`) REFERENCES `route` (`id`),
  ADD CONSTRAINT `parkinglot_ibfk_3` FOREIGN KEY (`State`) REFERENCES `parkinglotstate` (`id`);

--
-- Constraints for table `reservation`
--
ALTER TABLE `reservation`
  ADD CONSTRAINT `reservation_ibfk_1` FOREIGN KEY (`fk_parkingLot`) REFERENCES `parkinglot` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
