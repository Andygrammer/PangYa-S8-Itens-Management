CREATE DEFINER=`root`@`localhost` PROCEDURE `Z_IMGT_GetAllUsers`()
BEGIN
SELECT UID, NICK FROM pangya.account;
END