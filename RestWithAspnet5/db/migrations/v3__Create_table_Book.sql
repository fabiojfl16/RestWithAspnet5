CREATE TABLE IF NOT EXISTS `book` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `title` varchar(80) NOT NULL,
  `author` varchar(80) NOT NULL,
  `price` decimal NOT NULL,
  `launch_date` datetime NOT NULL,
  PRIMARY KEY (`id`)
);