CREATE DATABASE  IF NOT EXISTS `network_management` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `network_management`;
-- MySQL dump 10.13  Distrib 5.7.9, for Win64 (x86_64)
--
-- Host: 192.168.32.145    Database: network_management
-- ------------------------------------------------------
-- Server version	5.7.17

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Temporary view structure for view `view_iftable`
--

DROP TABLE IF EXISTS `view_iftable`;
/*!50001 DROP VIEW IF EXISTS `view_iftable`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `view_iftable` AS SELECT 
 1 AS `device_ip`,
 1 AS `ifIndex`,
 1 AS `ifDescr`,
 1 AS `PhysAddress`,
 1 AS `ifSpeed`,
 1 AS `ifAdminStatus`,
 1 AS `ifOperStatus`,
 1 AS `timestamp`,
 1 AS `device_id`,
 1 AS `sysName`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `view_dot1dbaseport_ifindex`
--

DROP TABLE IF EXISTS `view_dot1dbaseport_ifindex`;
/*!50001 DROP VIEW IF EXISTS `view_dot1dbaseport_ifindex`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `view_dot1dbaseport_ifindex` AS SELECT 
 1 AS `device_ip`,
 1 AS `ifIndex`,
 1 AS `ifDescr`,
 1 AS `dot1dBasePort`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `view_arp1`
--

DROP TABLE IF EXISTS `view_arp1`;
/*!50001 DROP VIEW IF EXISTS `view_arp1`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `view_arp1` AS SELECT 
 1 AS `ipNetToMediaPhysAddress`,
 1 AS `ipNetToMediaNetAddress`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `view_iftable_mac_count`
--

DROP TABLE IF EXISTS `view_iftable_mac_count`;
/*!50001 DROP VIEW IF EXISTS `view_iftable_mac_count`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `view_iftable_mac_count` AS SELECT 
 1 AS `device_ip`,
 1 AS `ifindex`,
 1 AS `ifDescr`,
 1 AS `mac_count`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `view_mac_address_table1`
--

DROP TABLE IF EXISTS `view_mac_address_table1`;
/*!50001 DROP VIEW IF EXISTS `view_mac_address_table1`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `view_mac_address_table1` AS SELECT 
 1 AS `dot1dTpFdbAddress`,
 1 AS `dot1dTpFdbPort`,
 1 AS `dot1dTpFdbStatus`,
 1 AS `Timestamp`,
 1 AS `device_ip`,
 1 AS `vlan`,
 1 AS `ifDescr`,
 1 AS `ifindex`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `view_detect_arp`
--

DROP TABLE IF EXISTS `view_detect_arp`;
/*!50001 DROP VIEW IF EXISTS `view_detect_arp`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `view_detect_arp` AS SELECT 
 1 AS `ipNetToMediaPhysAddress`,
 1 AS `num`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `view_arp_table`
--

DROP TABLE IF EXISTS `view_arp_table`;
/*!50001 DROP VIEW IF EXISTS `view_arp_table`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `view_arp_table` AS SELECT 
 1 AS `device_ip`,
 1 AS `ipNetToMediaIfIndex`,
 1 AS `ipNetToMediaNetAddress`,
 1 AS `ipNetToMediaPhysAddress`,
 1 AS `ipNetMediaType`,
 1 AS `timestamp`,
 1 AS `ifDescr`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `view_mac_address_table`
--

DROP TABLE IF EXISTS `view_mac_address_table`;
/*!50001 DROP VIEW IF EXISTS `view_mac_address_table`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `view_mac_address_table` AS SELECT 
 1 AS `dot1dTpFdbAddress`,
 1 AS `dot1dTpFdbPort`,
 1 AS `dot1dTpFdbStatus`,
 1 AS `Timestamp`,
 1 AS `device_ip`,
 1 AS `vlan`,
 1 AS `ifDescr`,
 1 AS `ifindex`,
 1 AS `sysName`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `view_log`
--

DROP TABLE IF EXISTS `view_log`;
/*!50001 DROP VIEW IF EXISTS `view_log`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `view_log` AS SELECT 
 1 AS `id`,
 1 AS `device_ip`,
 1 AS `log_type_id`,
 1 AS `timestamp`,
 1 AS `log_content`*/;
SET character_set_client = @saved_cs_client;

--
-- Final view structure for view `view_iftable`
--

/*!50001 DROP VIEW IF EXISTS `view_iftable`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`192.168.32.62` SQL SECURITY DEFINER */
/*!50001 VIEW `view_iftable` AS select `iftable`.`device_ip` AS `device_ip`,`iftable`.`ifIndex` AS `ifIndex`,`iftable`.`ifDescr` AS `ifDescr`,`iftable`.`PhysAddress` AS `PhysAddress`,`iftable`.`ifSpeed` AS `ifSpeed`,`iftable`.`ifAdminStatus` AS `ifAdminStatus`,`iftable`.`ifOperStatus` AS `ifOperStatus`,`iftable`.`timestamp` AS `timestamp`,`device`.`id` AS `device_id`,`device`.`sysName` AS `sysName` from (`iftable` join `device` on((`device`.`device_ip` = `iftable`.`device_ip`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `view_dot1dbaseport_ifindex`
--

/*!50001 DROP VIEW IF EXISTS `view_dot1dbaseport_ifindex`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`192.168.32.62` SQL SECURITY DEFINER */
/*!50001 VIEW `view_dot1dbaseport_ifindex` AS select `i`.`device_ip` AS `device_ip`,`i`.`ifIndex` AS `ifIndex`,`i`.`ifDescr` AS `ifDescr`,`d`.`dot1dBasePort` AS `dot1dBasePort` from (`iftable` `i` join `dot1dbaseport` `d` on(((`i`.`device_ip` = `d`.`device_ip`) and (`i`.`ifIndex` = `d`.`dot1dBasePortIfIndex`)))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `view_arp1`
--

/*!50001 DROP VIEW IF EXISTS `view_arp1`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `view_arp1` AS select distinct `arp`.`ipNetToMediaPhysAddress` AS `ipNetToMediaPhysAddress`,`arp`.`ipNetToMediaNetAddress` AS `ipNetToMediaNetAddress` from `arp` where ((not(`arp`.`ipNetToMediaPhysAddress` in (select `mac_whitelist`.`PhysAddress` from `mac_whitelist`))) and (`arp`.`timestamp` > (now() - interval 3 day))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `view_iftable_mac_count`
--

/*!50001 DROP VIEW IF EXISTS `view_iftable_mac_count`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `view_iftable_mac_count` AS select `view_mac_address_table`.`device_ip` AS `device_ip`,`view_mac_address_table`.`ifindex` AS `ifindex`,`view_mac_address_table`.`ifDescr` AS `ifDescr`,count(0) AS `mac_count` from `view_mac_address_table` where (`view_mac_address_table`.`ifindex` is not null) group by `view_mac_address_table`.`device_ip`,`view_mac_address_table`.`ifindex`,`view_mac_address_table`.`ifDescr` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `view_mac_address_table1`
--

/*!50001 DROP VIEW IF EXISTS `view_mac_address_table1`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`192.168.32.62` SQL SECURITY DEFINER */
/*!50001 VIEW `view_mac_address_table1` AS select `d`.`dot1dTpFdbAddress` AS `dot1dTpFdbAddress`,`d`.`dot1dTpFdbPort` AS `dot1dTpFdbPort`,`d`.`dot1dTpFdbStatus` AS `dot1dTpFdbStatus`,`d`.`Timestamp` AS `Timestamp`,`d`.`device_ip` AS `device_ip`,`d`.`vlan` AS `vlan`,`v`.`ifDescr` AS `ifDescr`,`v`.`ifIndex` AS `ifindex` from (`dot1dtpfdbtable` `d` left join `view_dot1dbaseport_ifindex` `v` on(((`d`.`device_ip` = `v`.`device_ip`) and (`d`.`dot1dTpFdbPort` = `v`.`dot1dBasePort`)))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `view_detect_arp`
--

/*!50001 DROP VIEW IF EXISTS `view_detect_arp`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`192.168.32.62` SQL SECURITY DEFINER */
/*!50001 VIEW `view_detect_arp` AS select `view_arp1`.`ipNetToMediaPhysAddress` AS `ipNetToMediaPhysAddress`,count(0) AS `num` from `view_arp1` group by `view_arp1`.`ipNetToMediaPhysAddress` order by `num` desc */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `view_arp_table`
--

/*!50001 DROP VIEW IF EXISTS `view_arp_table`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`192.168.32.62` SQL SECURITY DEFINER */
/*!50001 VIEW `view_arp_table` AS select `ip`.`device_ip` AS `device_ip`,`ip`.`ipNetToMediaIfIndex` AS `ipNetToMediaIfIndex`,`ip`.`ipNetToMediaNetAddress` AS `ipNetToMediaNetAddress`,`ip`.`ipNetToMediaPhysAddress` AS `ipNetToMediaPhysAddress`,`ip`.`ipNetMediaType` AS `ipNetMediaType`,`ip`.`timestamp` AS `timestamp`,`iftable`.`ifDescr` AS `ifDescr` from (`ipnettomediatable` `ip` join `iftable` on(((`ip`.`device_ip` = `iftable`.`device_ip`) and (`iftable`.`ifIndex` = `ip`.`ipNetToMediaIfIndex`)))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `view_mac_address_table`
--

/*!50001 DROP VIEW IF EXISTS `view_mac_address_table`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`192.168.32.62` SQL SECURITY DEFINER */
/*!50001 VIEW `view_mac_address_table` AS select `d`.`dot1dTpFdbAddress` AS `dot1dTpFdbAddress`,`d`.`dot1dTpFdbPort` AS `dot1dTpFdbPort`,`d`.`dot1dTpFdbStatus` AS `dot1dTpFdbStatus`,`d`.`Timestamp` AS `Timestamp`,`d`.`device_ip` AS `device_ip`,`d`.`vlan` AS `vlan`,`v`.`ifDescr` AS `ifDescr`,`v`.`ifIndex` AS `ifindex`,`dev`.`sysName` AS `sysName` from ((`dot1dtpfdbtable` `d` left join `view_dot1dbaseport_ifindex` `v` on(((`d`.`device_ip` = `v`.`device_ip`) and (`d`.`dot1dTpFdbPort` = `v`.`dot1dBasePort`)))) join `device` `dev` on((`d`.`device_ip` = `dev`.`device_ip`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `view_log`
--

/*!50001 DROP VIEW IF EXISTS `view_log`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `view_log` AS select `l`.`id` AS `id`,`l`.`device_ip` AS `device_ip`,`l`.`log_type_id` AS `log_type_id`,`l`.`timestamp` AS `timestamp`,`t`.`log_content` AS `log_content` from (`log` `l` join `log_type` `t` on((`l`.`log_type_id` = `t`.`id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Dumping routines for database 'network_management'
--
/*!50003 DROP PROCEDURE IF EXISTS `delete_outdate_data` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `delete_outdate_data`(`outdate_datetime` datetime)
BEGIN
	delete from iftable where TIMESTAMP<outdate_datetime;
	delete from arp where TIMESTAMP<outdate_datetime;
	delete from log where TIMESTAMP<outdate_datetime;
	delete from dot1dtpfdbtable where TIMESTAMP<outdate_datetime;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `detect_mac_switch_port` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`192.168.32.62` PROCEDURE `detect_mac_switch_port`(v_mac char(12))
BEGIN
select device_ip,sysName,ifindex,ifdescr , count(*) as num 
from view_mac_address_table where
 concat(device_ip,ifindex) in
 ( select concat (device_ip,ifindex) from view_mac_address_table
 where dot1dTpFdbAddress=v_mac)
group by device_ip,sysName,ifindex,ifdescr order by num ;



END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `insert_ARP` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`192.168.32.62` PROCEDURE `insert_ARP`(IN `p_ipNetToMediaPhysAddress` varchar(30),
	IN `p_ipNetToMediaNetAddress` char(15) ,
	IN `p_ipNetMediaType` smallint)
BEGIN
	declare temp_timestamp timestamp;	
	declare temp_ipNetToMediaPhysAddress varchar(30);
	declare temp_ipNetToMediaNetAddress char(15);
	declare temp_id int;
	
	/*多线程并发环境 同一个 MAC地址 ARP表 可能多个 tmiestamp相同的记录，特别是 速度很快 不能用timestamp判断新 */

  /* 先检查白名单 减小数据量， 因为读白名单内存缓冲，CPU负荷不高*/
	if not  exists  ( select * from  mac_whitelist  where PhysAddress= p_ipNetToMediaPhysAddress ) then	 
			 /* iD自动编号，所以ID最大 就是最新  这样性能快*/
		select max(id) into temp_id from arp where ipNetToMediaPhysAddress=p_ipNetToMediaPhysAddress;
		
		IF(temp_id is null) THEN
			/*新的MAC */
			insert into arp   (  ipNetToMediaPhysAddress, ipNetToMediaNetAddress, ipNetMediaType )
					 values(p_ipNetToMediaPhysAddress,p_ipNetToMediaNetAddress,p_ipNetMediaType ); 
		ELSE
			/*该MAC数据库中已经存在 */
			select ipNetToMediaNetAddress,ipNetToMediaPhysAddress into temp_ipNetToMediaNetAddress,temp_ipNetToMediaPhysAddress from  arp  
				where id=temp_id ; 	
		
			IF (temp_ipNetToMediaNetAddress is not null and temp_ipNetToMediaNetAddress=p_ipNetToMediaNetAddress  ) THEN
				/*同一个MAC 的IP 没有改变 相等更新时间 */
				update  arp set timestamp=CURRENT_TIMESTAMP  where  id=temp_id;
			ELSE
			/*不相等，插入 */
				insert into arp   (  ipNetToMediaPhysAddress, ipNetToMediaNetAddress, ipNetMediaType )
						 values(p_ipNetToMediaPhysAddress,p_ipNetToMediaNetAddress,p_ipNetMediaType ); 
			END IF; 

		END IF; 

   
	end if;


END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `insert_ARP_new` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `insert_ARP_new`(IN `p_ipNetToPhysicalPhysAddress` varchar(80),
	IN `p_ipNetToPhysicalNetAddress` varchar(80),	IN `p_ipNetToPhysicalType` smallint,
	IN `p_ipNetToPhysicalState` smallint,IN `p_ipNetToPhysicalRowStatus` smallint)
BEGIN
	declare temp_timestamp timestamp;	
	declare temp_ipNetToPhysicalPhysAddress varchar(30);
	declare temp_ipNetToPhysicalNetAddress varchar(80);
	declare temp_id int;
	
	/*多线程并发环境 同一个 MAC地址 ARP表 可能多个 tmiestamp相同的记录，特别是 速度很快 不能用timestamp判断新 */

  /* 先检查白名单 减小数据量， 因为读白名单内存缓冲，CPU负荷不高*/
	if not  exists  ( select * from  mac_whitelist  where PhysAddress= p_ipNetToPhysicalPhysAddress ) then	 
			 /* iD自动编号，所以ID最大 就是最新  这样性能快*/
		select max(id) into temp_id from ipnettophysicalphysaddress where ipNetToPhysicalPhysAddress=p_ipNetToPhysicalPhysAddress;
		IF(temp_id is null) THEN

			/*新的MAC */
			insert into ipnettophysicalphysaddress(ipNetToPhysicalPhysAddress, ipNetToPhysicalNetAddress, ipNetToPhysicalType,ipNetToPhysicalState,ipNetToPhysicalRowStatus)
					 values(p_ipNetToPhysicalPhysAddress,p_ipNetToPhysicalNetAddress,p_ipNetToPhysicalType,p_ipNetToPhysicalState,p_ipNetToPhysicalRowStatus); 

		ELSE
			/*该MAC数据库中已经存在 */
			select ipNetToPhysicalNetAddress,ipNetToPhysicalPhysAddress into temp_ipNetToPhysicalNetAddress,temp_ipNetToPhysicalPhysAddress from ipnettophysicalphysaddress  
				where id=temp_id ; 	
		
			IF (temp_ipNetToPhysicalNetAddress is not null and temp_ipNetToPhysicalNetAddress=p_ipNetToPhysicalNetAddress) THEN
				/*同一个MAC 的IP 没有改变 相等更新时间 */
				update ipnettophysicalphysaddress set timestamp=CURRENT_TIMESTAMP where id=temp_id;
			ELSE
			/*不相等，插入 */
				insert into ipnettophysicalphysaddress(ipNetToPhysicalPhysAddress, ipNetToPhysicalNetAddress, ipNetToPhysicalType,ipNetToPhysicalState,ipNetToPhysicalRowStatus)
					 values(p_ipNetToPhysicalPhysAddress,p_ipNetToPhysicalNetAddress,p_ipNetToPhysicalType,p_ipNetToPhysicalState,p_ipNetToPhysicalRowStatus); 

			END IF; 
		END IF;   
	end if;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `insert_dot1dBasePort` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`192.168.32.62` PROCEDURE `insert_dot1dBasePort`(p_device_ip varchar(15),p_dot1BasePort int,
p_dot1BasePortIfIndex int)
BEGIN
insert into dot1dbaseport   (device_ip, dot1dBasePort, dot1dBasePortIfIndex)
 values(p_device_ip, p_dot1BasePort, p_dot1BasePortIfIndex ); 

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `insert_dot1dtpfdbtable` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`192.168.32.62` PROCEDURE `insert_dot1dtpfdbtable`(p_device_ip varchar(15),
p_dot1dtpfdbAddress varchar(25),
p_dot1dtpfdbPort int,
p_dot1dtpfdbStatus int ,
p_vlan int)
BEGIN
	declare temp_timestamp timestamp;	
	declare temp_dot1dtpfdbAddress varchar(25);
	declare temp_device_ip varchar(25);
	declare temp_dot1dTpFdbPort int;
	declare temp_vlan int;
	declare temp_dot1dtpfdbStatus int;
    
	 /*最后更新的 同一个MAC的时间 */
	IF exists  ( select * from  dot1dtpfdbtable  where 
		dot1dtpfdbAddress=p_dot1dtpfdbAddress and device_ip=p_device_ip and dot1dTpFdbPort= p_dot1dtpfdbPort ) THEN
		update  dot1dtpfdbtable  set timestamp=CURRENT_TIMESTAMP  where 
			dot1dtpfdbAddress=p_dot1dtpfdbAddress  and device_ip=p_device_ip and dot1dTpFdbPort= p_dot1dtpfdbPort ;	
	ELSE
			insert into dot1dtpfdbtable (device_ip,dot1dtpfdbAddress,dot1dtpfdbPort,dot1dtpfdbStatus,vlan )
				values(p_device_ip,p_dot1dtpfdbAddress,p_dot1dtpfdbPort,p_dot1dtpfdbStatus,p_vlan); 
		 
	END IF;
	 

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `insert_ifTable` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `insert_ifTable`(p_device_ip varchar(15),p_ifIndex int,p_ifDescr varchar(255),p_ifSpeed bigint,p_physAddress char(12),p_ifAdminStatus tinyint,p_ifOperStatus tinyint)
BEGIN
		declare temp_id int;

if exists  ( select * from  iftable  where device_ip=p_device_ip and ifIndex=p_ifindex  ) then	 
	update  iftable  set ifDescr=p_ifDescr,PhysAddress=p_PhysAddress,ifSpeed=p_ifSpeed, ifAdminStatus=p_ifAdminStatus,ifOperStatus=p_ifOperStatus , timestamp=CURRENT_TIMESTAMP 
		where   device_ip=p_device_ip and ifIndex=p_ifindex ;	
else
  insert into iftable   (device_ip,ifIndex,ifDescr,PhysAddress,ifSpeed, ifAdminStatus,ifOperStatus)
			values (p_device_ip,p_ifIndex,p_ifDescr,p_PhysAddress,p_ifSpeed, p_ifAdminStatus,p_ifOperStatus); 
   
end if;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `insert_ipnettomediatable` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`192.168.32.62` PROCEDURE `insert_ipnettomediatable`(IN `p_device_ip` VARchar(15),
	IN `p_ipNetToMediaIfIndex` int,
	IN `p_ipNetToMediaPhysAddress` varchar(30),
	IN `p_ipNetToMediaNetAddress` char(15) ,
	IN `p_ipNetMediaType` smallint)
BEGIN
	declare temp_timestamp timestamp;	
	declare temp_ipNetToMediaPhysAddress varchar(30);
	declare temp_ipNetToMediaNetAddress char(15);
	declare temp_device_ip varchar(15);	
	
	 /*最后更新的 同一个MAC的时间 */
	select max(timestamp) into temp_timestamp from  ipnettomediatable where ipNetToMediaPhysAddress=p_ipNetToMediaPhysAddress;
	/*最后更新的 同一个MAC的数据*/
	select device_ip,ipNetToMediaNetAddress,ipNetToMediaPhysAddress into temp_device_ip,temp_ipNetToMediaNetAddress,temp_ipNetToMediaPhysAddress from  ipnettomediatable
		where ipNetToMediaPhysAddress=p_ipNetToMediaPhysAddress and timestamp=temp_timestamp order by timestamp desc  limit 1 ; 
  	 

	/*同一个MAC 的IP 没有改变 */
	IF (temp_ipNetToMediaNetAddress is not null and temp_ipNetToMediaNetAddress=p_ipNetToMediaNetAddress and temp_device_ip=p_device_ip ) THEN
		/*相等更新时间*/
		update  ipnettomediatable  set timestamp=CURRENT_TIMESTAMP 
		 where  ipNetToMediaPhysAddress=p_ipNetToMediaPhysAddress and timestamp=temp_timestamp;
    ELSE
		/*不相等，插入 */
		insert into ipnettomediatable   (device_ip, ipNetToMediaIfIndex, ipNetToMediaPhysAddress, ipNetToMediaNetAddress, ipNetMediaType )
		 values(p_device_ip,p_ipNetToMediaIfIndex,p_ipNetToMediaPhysAddress,p_ipNetToMediaNetAddress,p_ipNetMediaType ); 
   END IF;


END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `mac_whitelist_from_iftable` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`192.168.32.62` PROCEDURE `mac_whitelist_from_iftable`()
BEGIN
insert into mac_whitelist (PhysAddress)   
	(SELECT  distinct PhysAddress FROM iftable where device_ip in (select device_ip from device where get_arp=1)
		and PhysAddress not in (select PhysAddress from mac_whitelist)
		and PhysAddress<>'' and PhysAddress<>'000000000000'
 );
 
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-08-04 12:24:24
