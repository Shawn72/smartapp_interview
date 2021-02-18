-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Feb 18, 2021 at 08:47 AM
-- Server version: 10.1.10-MariaDB
-- PHP Version: 7.0.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `smartapp_db`
--

-- --------------------------------------------------------

--
-- Table structure for table `bills`
--

CREATE TABLE `bills` (
  `id` int(11) NOT NULL,
  `description` varchar(100) NOT NULL,
  `bill_account_no` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `bills`
--

INSERT INTO `bills` (`id`, `description`, `bill_account_no`) VALUES
(1, 'KPLC', '888888'),
(2, 'DSTV', '666888');

-- --------------------------------------------------------

--
-- Table structure for table `general_transactions`
--

CREATE TABLE `general_transactions` (
  `id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `account_id` int(11) NOT NULL,
  `transaction_description` varchar(100) NOT NULL,
  `source_account` varchar(100) NOT NULL,
  `account_number_paid_to` varchar(100) NOT NULL,
  `transaction_amount` decimal(10,2) NOT NULL,
  `transaction_type` varchar(100) NOT NULL,
  `transaction_date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `general_transactions`
--

INSERT INTO `general_transactions` (`id`, `user_id`, `account_id`, `transaction_description`, `source_account`, `account_number_paid_to`, `transaction_amount`, `transaction_type`, `transaction_date`) VALUES
(1, 1, 1, 'Deposit to Account', '1155990088', '1155990088', '50000.00', 'Deposit', '2021-02-17 00:11:31'),
(2, 1, 1, 'Deposit to Account', '1155990088', '1155990088', '5000.00', 'Deposit', '2021-02-18 00:29:46'),
(3, 1, 1, 'Deposit to Account', '1155990088', '1155990088', '6000.00', 'Deposit', '2021-02-18 00:31:24'),
(4, 1, 1, 'Deposit to Account', '1155990088', '1155990088', '9000.00', 'Deposit', '2021-02-18 00:32:17'),
(5, 1, 1, 'Deposit to Account', '1155990088', '1155990088', '67900.00', 'Deposit', '2021-02-18 00:40:35'),
(6, 1, 1, 'Withdraw to Account', '1155990088', '1155990088', '5000.00', 'Withdraw', '2021-02-18 00:44:31'),
(7, 1, 1, 'Deposit to Account', '1155990088', '1155990088', '4500.00', 'Deposit', '2021-02-18 08:51:08'),
(8, 1, 1, 'Pay Bill from Account', '1155990088', '888888', '450.00', 'Bill Payment', '2021-02-18 09:30:30'),
(9, 1, 1, 'Pay Bill from Account', '1155990088', '666888', '50.00', 'Bill Payment', '2021-02-18 09:30:42'),
(10, 1, 1, 'Deposit to Account', '1155990088', '1155990088', '4000.00', 'Deposit', '2021-02-18 10:14:35'),
(11, 1, 1, 'Withdraw from Account', '1155990088', '1155990088', '900.00', 'Withdraw', '2021-02-18 10:14:58'),
(12, 1, 1, 'Pay Bill from Account to: KPLC', '1155990088', '888888', '70.00', 'Bill Payment', '2021-02-18 10:15:34');

-- --------------------------------------------------------

--
-- Table structure for table `investments`
--

CREATE TABLE `investments` (
  `id` int(11) NOT NULL,
  `investment_code` varchar(100) NOT NULL,
  `investment_description` varchar(100) NOT NULL,
  `investment_value` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `investments`
--

INSERT INTO `investments` (`id`, `investment_code`, `investment_description`, `investment_value`) VALUES
(1, 'SHB2021', 'Land Portion In Kile', '3400000.00'),
(2, 'PSH3030', 'Car Savings', '600000.00');

-- --------------------------------------------------------

--
-- Table structure for table `investment_transactions`
--

CREATE TABLE `investment_transactions` (
  `id` int(11) NOT NULL,
  `investment_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `user_account_id` int(11) NOT NULL,
  `transaction_value` decimal(10,2) NOT NULL,
  `investment_value_balance` decimal(10,2) NOT NULL,
  `investment_transaction_type` varchar(100) NOT NULL,
  `transaction_date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `user_id` int(11) NOT NULL,
  `fname` varchar(100) NOT NULL,
  `mname` varchar(100) NOT NULL,
  `lname` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `id_number` varchar(20) NOT NULL,
  `mobile_number` varchar(20) NOT NULL,
  `password` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`user_id`, `fname`, `mname`, `lname`, `email`, `id_number`, `mobile_number`, `password`) VALUES
(1, 'Shawn', 'Mbuvi', 'Ngutu', 'shawnmbuvi@gmail.com', '29501445', '0731628560', 'vE41b+4ZciQsj36r9N/1Fw=='),
(6, 'jeff', 's', 'ngutu', 'shawnmbuvi30@gmail.com', '23265232', '13324443434', 'tZxnvxlqR1gZHkL3ZnDOug=='),
(7, 'Mike', 'T', 'Lockwolf', 'seanmbuvi72@dynasoft.co.ke', '', '', 'tZxnvxlqR1gZHkL3ZnDOug==');

-- --------------------------------------------------------

--
-- Table structure for table `user_bank_details`
--

CREATE TABLE `user_bank_details` (
  `id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `account_number` varchar(100) NOT NULL,
  `account_name` varchar(100) NOT NULL,
  `account_balance` decimal(10,2) NOT NULL,
  `account_status` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `user_bank_details`
--

INSERT INTO `user_bank_details` (`id`, `user_id`, `account_number`, `account_name`, `account_balance`, `account_status`) VALUES
(1, 1, '1155990088', 'Shawn Ngutu', '139930.00', 'active'),
(2, 6, '556666', 'jeff ngutu', '0.00', 'active'),
(3, 7, '66889084', 'Mike Lockwolf', '0.00', 'inactive'),
(4, 1, '342453535', 'Shawn Ngutu', '34500.00', 'inactive'),
(5, 1, '4567890', 'Shawn Ngutu', '5000.00', 'inactive');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `bills`
--
ALTER TABLE `bills`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `general_transactions`
--
ALTER TABLE `general_transactions`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_trxto_bank_details` (`account_id`),
  ADD KEY `fk_trxto_users` (`user_id`);

--
-- Indexes for table `investments`
--
ALTER TABLE `investments`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `inestment_code` (`investment_code`);

--
-- Indexes for table `investment_transactions`
--
ALTER TABLE `investment_transactions`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_trxto_investments` (`investment_id`),
  ADD KEY `fk_invtrxto_users` (`user_id`),
  ADD KEY `fk_invtrxto_bankdetails` (`user_account_id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`user_id`),
  ADD UNIQUE KEY `email` (`email`);

--
-- Indexes for table `user_bank_details`
--
ALTER TABLE `user_bank_details`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `account_number` (`account_number`),
  ADD KEY `user_id` (`user_id`) USING BTREE;

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `bills`
--
ALTER TABLE `bills`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT for table `general_transactions`
--
ALTER TABLE `general_transactions`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;
--
-- AUTO_INCREMENT for table `investments`
--
ALTER TABLE `investments`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT for table `investment_transactions`
--
ALTER TABLE `investment_transactions`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;
--
-- AUTO_INCREMENT for table `user_bank_details`
--
ALTER TABLE `user_bank_details`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `general_transactions`
--
ALTER TABLE `general_transactions`
  ADD CONSTRAINT `fk_trxto_bank_details` FOREIGN KEY (`account_id`) REFERENCES `user_bank_details` (`id`),
  ADD CONSTRAINT `fk_trxto_users` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`);

--
-- Constraints for table `investment_transactions`
--
ALTER TABLE `investment_transactions`
  ADD CONSTRAINT `fk_invtrxto_bankdetails` FOREIGN KEY (`user_account_id`) REFERENCES `user_bank_details` (`id`),
  ADD CONSTRAINT `fk_invtrxto_users` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`),
  ADD CONSTRAINT `fk_trxto_investments` FOREIGN KEY (`investment_id`) REFERENCES `investments` (`id`);

--
-- Constraints for table `user_bank_details`
--
ALTER TABLE `user_bank_details`
  ADD CONSTRAINT `fk_user_id` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
