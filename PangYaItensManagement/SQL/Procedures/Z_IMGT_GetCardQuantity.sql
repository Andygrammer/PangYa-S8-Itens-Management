CREATE DEFINER=`root`@`localhost` PROCEDURE `Z_IMGT_GetCardQuantity`(IN `USERID` int, IN `CARDID` int)
BEGIN
SELECT QNTD FROM pangya.pangya_card
WHERE
UID = USERID
AND
card_typeid = CARDID;
END