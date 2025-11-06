CREATE SCHEMA IF NOT EXISTS `sistema_de_agendamento` DEFAULT CHARACTER SET utf8;

CREATE TABLE IF NOT EXISTS `sistema_de_agendamento`.`role` (
  `role_id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`role_id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `sistema_de_agendamento`.`user` (
  `user_id` INT NOT NULL AUTO_INCREMENT,
  `username` VARCHAR(100) NOT NULL,
  `password` VARCHAR(100) NOT NULL,
  `role_id` INT NOT NULL,
  PRIMARY KEY (`user_id`),
  CONSTRAINT `user_role`
    FOREIGN KEY (`role_id`)
    REFERENCES `sistema_de_agendamento`.`role` (`role_id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `sistema_de_agendamento`.`employee` (
  `employee_id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(100) NOT NULL,
  `phone_number` VARCHAR(25) NOT NULL,
  `email` VARCHAR(100),
  `user_id` INT NOT NULL,
  PRIMARY KEY (`employee_id`),
  CONSTRAINT `employee_user`
    FOREIGN KEY (`user_id`)
    REFERENCES `sistema_de_agendamento`.`user` (`user_id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `sistema_de_agendamento`.`availability` (
  `availability_id` INT NOT NULL AUTO_INCREMENT,
  `weekday` ENUM('monday', 'tuesday', 'wednesday', 'thursday', 'friday', 'saturday', 'sunday') NOT NULL,
  `start_time` TIME,
  `end_time` TIME,
  `is_active` TINYINT,
  `employee_id` INT NOT NULL,
  PRIMARY KEY (`availability_id`),
  CONSTRAINT `availability_employee`
    FOREIGN KEY (`employee_id`)
    REFERENCES `sistema_de_agendamento`.`employee` (`employee_id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `sistema_de_agendamento`.`client` (
  `client_id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(100) NOT NULL,
  `phone_number` VARCHAR(25) NOT NULL,
  PRIMARY KEY (`client_id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `sistema_de_agendamento`.`service` (
  `service_id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `price` DECIMAL(10,2) NOT NULL,
  `duration` INT NOT NULL,
  PRIMARY KEY (`service_id`))
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `sistema_de_agendamento`.`appointment` (
  `appointment_id` INT NOT NULL AUTO_INCREMENT,
  `client_id` INT NOT NULL,
  `service_id` INT NOT NULL,
  `status` ENUM('scheduled', 'canceled', 'completed') NOT NULL,
  `created_at` DATETIME NOT NULL,
  `start_time` DATETIME NOT NULL,
  `end_time` DATETIME NOT NULL,
  `employee_id` INT NOT NULL,
  PRIMARY KEY (`appointment_id`),
  CONSTRAINT `appointment_client`
    FOREIGN KEY (`client_id`)
    REFERENCES `sistema_de_agendamento`.`client` (`client_id`),
  CONSTRAINT `appointment_employee`
    FOREIGN KEY (`employee_id`)
    REFERENCES `sistema_de_agendamento`.`employee` (`employee_id`),
  CONSTRAINT `appointment_service`
    FOREIGN KEY (`service_id`)
    REFERENCES `sistema_de_agendamento`.`service` (`service_id`))
ENGINE = InnoDB;

-- selecione o db antes de rodar esse ultimos comandos

INSERT INTO role (name) VALUES ('admin'), ('employee');

INSERT INTO user (username, password, role_id) VALUES ('admin', '123', 1);