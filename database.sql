CREATE DATABASE IF NOT EXISTS `tasks-db`;
USE `tasks-db`;
DROP TABLE IF EXISTS `TaskItems`;
CREATE TABLE IF NOT EXISTS `TaskItems` (`id` INTEGER NOT NULL auto_increment , `title` VARCHAR(255) NOT NULL, `description` VARCHAR(255), `createdAt` DATETIME DEFAULT CURRENT_TIMESTAMP, `updatedAt` DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, PRIMARY KEY (`id`));