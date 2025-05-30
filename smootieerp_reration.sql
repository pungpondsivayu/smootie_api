-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 15, 2025 at 12:16 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `smootieerp`
--

-- --------------------------------------------------------

--
-- Table structure for table `attendance_logs`
--

CREATE TABLE `attendance_logs` (
  `log_id` int(11) NOT NULL,
  `employee_id` int(11) DEFAULT NULL,
  `shift_id` int(11) DEFAULT NULL,
  `work_date` date DEFAULT NULL,
  `check_in_time` datetime DEFAULT NULL,
  `check_out_time` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `branches`
--

CREATE TABLE `branches` (
  `branch_id` int(11) NOT NULL,
  `branch_name` varchar(100) DEFAULT NULL,
  `province` varchar(100) DEFAULT NULL,
  `district` varchar(100) DEFAULT NULL,
  `sub_district` varchar(100) DEFAULT NULL,
  `postal_code` varchar(10) DEFAULT NULL,
  `address_detail` text DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `update_date` datetime DEFAULT NULL,
  `created_by` varchar(50) DEFAULT NULL,
  `updated_by` varchar(50) DEFAULT NULL,
  `is_used` tinyint(1) DEFAULT 1,
  `is_deleted` tinyint(1) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `coupons`
--

CREATE TABLE `coupons` (
  `coupon_id` int(11) NOT NULL,
  `code` varchar(50) DEFAULT NULL,
  `description` text DEFAULT NULL,
  `discount_type` varchar(50) DEFAULT NULL,
  `discount_value` decimal(10,2) DEFAULT NULL,
  `minimum_purchase` decimal(10,2) DEFAULT NULL,
  `minimum_count` int(11) DEFAULT NULL,
  `expiry_date` datetime DEFAULT NULL,
  `created_by` varchar(50) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `is_used` tinyint(1) DEFAULT 1,
  `is_deleted` tinyint(1) DEFAULT 0,
  `updated_by` varchar(50) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `customers`
--

CREATE TABLE `customers` (
  `customer_id` int(11) NOT NULL,
  `name` varchar(100) DEFAULT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `birthdate` date DEFAULT NULL,
  `password` varchar(255) DEFAULT NULL,
  `created_by` varchar(50) DEFAULT NULL,
  `updated_by` varchar(50) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  `is_used` tinyint(1) DEFAULT 1,
  `is_deleted` tinyint(1) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `customer_coupons`
--

CREATE TABLE `customer_coupons` (
  `customer_id` int(11) NOT NULL,
  `coupon_id` int(11) NOT NULL,
  `is_used` tinyint(1) DEFAULT 0,
  `assigned_date` datetime DEFAULT current_timestamp(),
  `used_date` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `customer_points`
--

CREATE TABLE `customer_points` (
  `customer_id` int(11) NOT NULL,
  `total_points` int(11) DEFAULT 0,
  `last_updated` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `employees`
--

CREATE TABLE `employees` (
  `employee_id` int(11) NOT NULL,
  `branch_id` int(11) DEFAULT NULL,
  `role_id` int(11) DEFAULT NULL,
  `fullname` varchar(100) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `phone_number` varchar(20) DEFAULT NULL,
  `password_hash` varchar(255) DEFAULT NULL,
  `hire_date` date DEFAULT NULL,
  `status` varchar(50) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `update_date` datetime DEFAULT NULL,
  `created_by` varchar(50) DEFAULT NULL,
  `updated_by` varchar(50) DEFAULT NULL,
  `is_used` tinyint(1) DEFAULT 1,
  `is_deleted` tinyint(1) DEFAULT 0,
  `image` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `employee_shifts`
--

CREATE TABLE `employee_shifts` (
  `esid` int(11) NOT NULL,
  `employee_id` int(11) DEFAULT NULL,
  `shift_id` int(11) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `update_date` datetime DEFAULT NULL,
  `created_by` varchar(50) DEFAULT NULL,
  `updated_by` varchar(50) DEFAULT NULL,
  `is_used` tinyint(1) DEFAULT 1,
  `is_deleted` tinyint(1) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `ingredients`
--

CREATE TABLE `ingredients` (
  `ingredient_id` int(11) NOT NULL,
  `name` varchar(100) DEFAULT NULL,
  `unit` varchar(50) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `update_date` datetime DEFAULT NULL,
  `created_by` varchar(50) DEFAULT NULL,
  `updated_by` varchar(50) DEFAULT NULL,
  `is_used` tinyint(1) DEFAULT 1,
  `is_deleted` tinyint(1) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `menus`
--

CREATE TABLE `menus` (
  `menu_id` int(11) NOT NULL,
  `name` varchar(100) DEFAULT NULL,
  `price` decimal(10,2) DEFAULT NULL,
  `created_by` varchar(50) DEFAULT NULL,
  `updated_by` varchar(50) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL,
  `is_used` tinyint(1) DEFAULT 1,
  `is_deleted` tinyint(1) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `menus`
--

INSERT INTO `menus` (`menu_id`, `name`, `price`, `created_by`, `updated_by`, `created_date`, `updated_date`, `is_used`, `is_deleted`) VALUES
(1, '123', 123.00, '123', '123', NULL, NULL, 1, 0);

-- --------------------------------------------------------

--
-- Table structure for table `menu_recipes`
--

CREATE TABLE `menu_recipes` (
  `recipe_id` int(11) NOT NULL,
  `menu_id` int(11) DEFAULT NULL,
  `ingredient_id` int(11) DEFAULT NULL,
  `quantity` decimal(10,2) DEFAULT NULL,
  `created_by` varchar(50) DEFAULT NULL,
  `updated_by` varchar(50) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `orders`
--

CREATE TABLE `orders` (
  `order_id` int(11) NOT NULL,
  `customer_id` int(11) DEFAULT NULL,
  `branch_id` int(11) DEFAULT NULL,
  `order_time` datetime DEFAULT NULL,
  `channel` varchar(50) DEFAULT NULL,
  `payment_method` varchar(50) DEFAULT NULL,
  `pickup_method` varchar(50) DEFAULT NULL,
  `total_amount` decimal(10,2) DEFAULT NULL,
  `discount_amount` decimal(10,2) DEFAULT 0.00,
  `final_amount` decimal(10,2) DEFAULT NULL,
  `coupon_id` int(11) DEFAULT NULL,
  `points_earned` int(11) DEFAULT 0,
  `points_used` int(11) DEFAULT 0,
  `status` varchar(50) DEFAULT NULL,
  `pickup_time` datetime DEFAULT NULL,
  `delivery_status` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `order_items`
--

CREATE TABLE `order_items` (
  `order_item_id` int(11) NOT NULL,
  `order_id` int(11) DEFAULT NULL,
  `menu_id` int(11) DEFAULT NULL,
  `quantity` int(11) DEFAULT NULL,
  `price` decimal(10,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `referrals`
--

CREATE TABLE `referrals` (
  `referral_id` int(11) NOT NULL,
  `referrer_id` int(11) DEFAULT NULL,
  `referred_id` int(11) DEFAULT NULL,
  `referred_date` datetime DEFAULT NULL,
  `reward_given` tinyint(1) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `roles`
--

CREATE TABLE `roles` (
  `role_id` int(11) NOT NULL,
  `role_name` varchar(50) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `update_date` datetime DEFAULT NULL,
  `created_by` varchar(50) DEFAULT NULL,
  `updated_by` varchar(50) DEFAULT NULL,
  `is_used` tinyint(1) DEFAULT 1,
  `is_deleted` tinyint(1) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `shifts`
--

CREATE TABLE `shifts` (
  `shift_id` int(11) NOT NULL,
  `branch_id` int(11) DEFAULT NULL,
  `shift_name` varchar(50) DEFAULT NULL,
  `start_time` time DEFAULT NULL,
  `end_time` time DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `update_date` datetime DEFAULT NULL,
  `created_by` varchar(50) DEFAULT NULL,
  `updated_by` varchar(50) DEFAULT NULL,
  `is_used` tinyint(1) DEFAULT 1,
  `is_deleted` tinyint(1) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `stock_items`
--

CREATE TABLE `stock_items` (
  `stock_id` int(11) NOT NULL,
  `branch_id` int(11) DEFAULT NULL,
  `ingredient_id` int(11) DEFAULT NULL,
  `quantity` decimal(10,2) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `stock_logs`
--

CREATE TABLE `stock_logs` (
  `log_id` int(11) NOT NULL,
  `employee_id` int(11) DEFAULT NULL,
  `change_type` varchar(50) DEFAULT NULL,
  `note` varchar(255) DEFAULT NULL,
  `quantity` int(11) DEFAULT NULL,
  `log_time` datetime DEFAULT NULL,
  `request_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `stock_requests`
--

CREATE TABLE `stock_requests` (
  `request_id` int(11) NOT NULL,
  `employee_id` int(11) DEFAULT NULL,
  `branch_id` int(11) DEFAULT NULL,
  `request_date` datetime DEFAULT NULL,
  `status` varchar(50) DEFAULT NULL,
  `change_type` varchar(50) DEFAULT NULL,
  `approved_by` varchar(50) DEFAULT NULL,
  `approved_date` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `stock_request_items`
--

CREATE TABLE `stock_request_items` (
  `item_id` int(11) NOT NULL,
  `request_id` int(11) DEFAULT NULL,
  `ingredient_id` int(11) DEFAULT NULL,
  `quantity` decimal(10,2) DEFAULT NULL,
  `created_by` varchar(50) DEFAULT NULL,
  `created_date` datetime DEFAULT NULL,
  `updated_by` varchar(50) DEFAULT NULL,
  `updated_date` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `attendance_logs`
--
ALTER TABLE `attendance_logs`
  ADD PRIMARY KEY (`log_id`),
  ADD KEY `employee_id` (`employee_id`),
  ADD KEY `shift_id` (`shift_id`);

--
-- Indexes for table `branches`
--
ALTER TABLE `branches`
  ADD PRIMARY KEY (`branch_id`);

--
-- Indexes for table `coupons`
--
ALTER TABLE `coupons`
  ADD PRIMARY KEY (`coupon_id`),
  ADD UNIQUE KEY `code` (`code`);

--
-- Indexes for table `customers`
--
ALTER TABLE `customers`
  ADD PRIMARY KEY (`customer_id`);

--
-- Indexes for table `customer_coupons`
--
ALTER TABLE `customer_coupons`
  ADD PRIMARY KEY (`customer_id`,`coupon_id`),
  ADD KEY `coupon_id` (`coupon_id`);

--
-- Indexes for table `customer_points`
--
ALTER TABLE `customer_points`
  ADD PRIMARY KEY (`customer_id`);

--
-- Indexes for table `employees`
--
ALTER TABLE `employees`
  ADD PRIMARY KEY (`employee_id`),
  ADD UNIQUE KEY `email` (`email`),
  ADD KEY `branch_id` (`branch_id`),
  ADD KEY `role_id` (`role_id`);

--
-- Indexes for table `employee_shifts`
--
ALTER TABLE `employee_shifts`
  ADD PRIMARY KEY (`esid`),
  ADD KEY `employee_id` (`employee_id`),
  ADD KEY `shift_id` (`shift_id`);

--
-- Indexes for table `ingredients`
--
ALTER TABLE `ingredients`
  ADD PRIMARY KEY (`ingredient_id`),
  ADD UNIQUE KEY `name` (`name`);

--
-- Indexes for table `menus`
--
ALTER TABLE `menus`
  ADD PRIMARY KEY (`menu_id`);

--
-- Indexes for table `menu_recipes`
--
ALTER TABLE `menu_recipes`
  ADD PRIMARY KEY (`recipe_id`),
  ADD KEY `menu_id` (`menu_id`),
  ADD KEY `ingredient_id` (`ingredient_id`);

--
-- Indexes for table `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`order_id`),
  ADD KEY `customer_id` (`customer_id`),
  ADD KEY `branch_id` (`branch_id`),
  ADD KEY `coupon_id` (`coupon_id`);

--
-- Indexes for table `order_items`
--
ALTER TABLE `order_items`
  ADD PRIMARY KEY (`order_item_id`),
  ADD KEY `order_id` (`order_id`),
  ADD KEY `menu_id` (`menu_id`);

--
-- Indexes for table `referrals`
--
ALTER TABLE `referrals`
  ADD PRIMARY KEY (`referral_id`),
  ADD KEY `referrer_id` (`referrer_id`),
  ADD KEY `referred_id` (`referred_id`);

--
-- Indexes for table `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`role_id`);

--
-- Indexes for table `shifts`
--
ALTER TABLE `shifts`
  ADD PRIMARY KEY (`shift_id`),
  ADD KEY `branch_id` (`branch_id`);

--
-- Indexes for table `stock_items`
--
ALTER TABLE `stock_items`
  ADD PRIMARY KEY (`stock_id`),
  ADD KEY `branch_id` (`branch_id`),
  ADD KEY `ingredient_id` (`ingredient_id`);

--
-- Indexes for table `stock_logs`
--
ALTER TABLE `stock_logs`
  ADD PRIMARY KEY (`log_id`),
  ADD KEY `employee_id` (`employee_id`),
  ADD KEY `request_id` (`request_id`);

--
-- Indexes for table `stock_requests`
--
ALTER TABLE `stock_requests`
  ADD PRIMARY KEY (`request_id`),
  ADD KEY `employee_id` (`employee_id`),
  ADD KEY `branch_id` (`branch_id`);

--
-- Indexes for table `stock_request_items`
--
ALTER TABLE `stock_request_items`
  ADD PRIMARY KEY (`item_id`),
  ADD KEY `request_id` (`request_id`),
  ADD KEY `ingredient_id` (`ingredient_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `attendance_logs`
--
ALTER TABLE `attendance_logs`
  MODIFY `log_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `branches`
--
ALTER TABLE `branches`
  MODIFY `branch_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `coupons`
--
ALTER TABLE `coupons`
  MODIFY `coupon_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `customers`
--
ALTER TABLE `customers`
  MODIFY `customer_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `employees`
--
ALTER TABLE `employees`
  MODIFY `employee_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `employee_shifts`
--
ALTER TABLE `employee_shifts`
  MODIFY `esid` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `ingredients`
--
ALTER TABLE `ingredients`
  MODIFY `ingredient_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `menus`
--
ALTER TABLE `menus`
  MODIFY `menu_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `menu_recipes`
--
ALTER TABLE `menu_recipes`
  MODIFY `recipe_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `orders`
--
ALTER TABLE `orders`
  MODIFY `order_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `order_items`
--
ALTER TABLE `order_items`
  MODIFY `order_item_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `referrals`
--
ALTER TABLE `referrals`
  MODIFY `referral_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `roles`
--
ALTER TABLE `roles`
  MODIFY `role_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `shifts`
--
ALTER TABLE `shifts`
  MODIFY `shift_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `stock_items`
--
ALTER TABLE `stock_items`
  MODIFY `stock_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `stock_logs`
--
ALTER TABLE `stock_logs`
  MODIFY `log_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `stock_requests`
--
ALTER TABLE `stock_requests`
  MODIFY `request_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `stock_request_items`
--
ALTER TABLE `stock_request_items`
  MODIFY `item_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `attendance_logs`
--
ALTER TABLE `attendance_logs`
  ADD CONSTRAINT `attendance_logs_ibfk_1` FOREIGN KEY (`employee_id`) REFERENCES `employees` (`employee_id`),
  ADD CONSTRAINT `attendance_logs_ibfk_2` FOREIGN KEY (`shift_id`) REFERENCES `shifts` (`shift_id`);

--
-- Constraints for table `customer_coupons`
--
ALTER TABLE `customer_coupons`
  ADD CONSTRAINT `customer_coupons_ibfk_1` FOREIGN KEY (`customer_id`) REFERENCES `customers` (`customer_id`),
  ADD CONSTRAINT `customer_coupons_ibfk_2` FOREIGN KEY (`coupon_id`) REFERENCES `coupons` (`coupon_id`);

--
-- Constraints for table `customer_points`
--
ALTER TABLE `customer_points`
  ADD CONSTRAINT `customer_points_ibfk_1` FOREIGN KEY (`customer_id`) REFERENCES `customers` (`customer_id`);

--
-- Constraints for table `employees`
--
ALTER TABLE `employees`
  ADD CONSTRAINT `employees_ibfk_1` FOREIGN KEY (`branch_id`) REFERENCES `branches` (`branch_id`),
  ADD CONSTRAINT `employees_ibfk_2` FOREIGN KEY (`role_id`) REFERENCES `roles` (`role_id`);

--
-- Constraints for table `employee_shifts`
--
ALTER TABLE `employee_shifts`
  ADD CONSTRAINT `employee_shifts_ibfk_1` FOREIGN KEY (`employee_id`) REFERENCES `employees` (`employee_id`),
  ADD CONSTRAINT `employee_shifts_ibfk_2` FOREIGN KEY (`shift_id`) REFERENCES `shifts` (`shift_id`);

--
-- Constraints for table `menu_recipes`
--
ALTER TABLE `menu_recipes`
  ADD CONSTRAINT `menu_recipes_ibfk_1` FOREIGN KEY (`menu_id`) REFERENCES `menus` (`menu_id`),
  ADD CONSTRAINT `menu_recipes_ibfk_2` FOREIGN KEY (`ingredient_id`) REFERENCES `ingredients` (`ingredient_id`);

--
-- Constraints for table `orders`
--
ALTER TABLE `orders`
  ADD CONSTRAINT `orders_ibfk_1` FOREIGN KEY (`customer_id`) REFERENCES `customers` (`customer_id`),
  ADD CONSTRAINT `orders_ibfk_3` FOREIGN KEY (`branch_id`) REFERENCES `branches` (`branch_id`),
  ADD CONSTRAINT `orders_ibfk_4` FOREIGN KEY (`coupon_id`) REFERENCES `coupons` (`coupon_id`);

--
-- Constraints for table `order_items`
--
ALTER TABLE `order_items`
  ADD CONSTRAINT `order_items_ibfk_1` FOREIGN KEY (`order_id`) REFERENCES `orders` (`order_id`),
  ADD CONSTRAINT `order_items_ibfk_2` FOREIGN KEY (`menu_id`) REFERENCES `menus` (`menu_id`);

--
-- Constraints for table `referrals`
--
ALTER TABLE `referrals`
  ADD CONSTRAINT `referrals_ibfk_1` FOREIGN KEY (`referrer_id`) REFERENCES `customers` (`customer_id`),
  ADD CONSTRAINT `referrals_ibfk_2` FOREIGN KEY (`referred_id`) REFERENCES `customers` (`customer_id`);

--
-- Constraints for table `shifts`
--
ALTER TABLE `shifts`
  ADD CONSTRAINT `shifts_ibfk_1` FOREIGN KEY (`branch_id`) REFERENCES `branches` (`branch_id`);

--
-- Constraints for table `stock_items`
--
ALTER TABLE `stock_items`
  ADD CONSTRAINT `stock_items_ibfk_1` FOREIGN KEY (`branch_id`) REFERENCES `branches` (`branch_id`),
  ADD CONSTRAINT `stock_items_ibfk_2` FOREIGN KEY (`ingredient_id`) REFERENCES `ingredients` (`ingredient_id`);

--
-- Constraints for table `stock_logs`
--
ALTER TABLE `stock_logs`
  ADD CONSTRAINT `stock_logs_ibfk_2` FOREIGN KEY (`employee_id`) REFERENCES `employees` (`employee_id`),
  ADD CONSTRAINT `stock_logs_ibfk_3` FOREIGN KEY (`request_id`) REFERENCES `stock_requests` (`request_id`);

--
-- Constraints for table `stock_requests`
--
ALTER TABLE `stock_requests`
  ADD CONSTRAINT `stock_requests_ibfk_1` FOREIGN KEY (`employee_id`) REFERENCES `employees` (`employee_id`),
  ADD CONSTRAINT `stock_requests_ibfk_2` FOREIGN KEY (`branch_id`) REFERENCES `branches` (`branch_id`);

--
-- Constraints for table `stock_request_items`
--
ALTER TABLE `stock_request_items`
  ADD CONSTRAINT `stock_request_items_ibfk_1` FOREIGN KEY (`request_id`) REFERENCES `stock_requests` (`request_id`),
  ADD CONSTRAINT `stock_request_items_ibfk_2` FOREIGN KEY (`ingredient_id`) REFERENCES `ingredients` (`ingredient_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
