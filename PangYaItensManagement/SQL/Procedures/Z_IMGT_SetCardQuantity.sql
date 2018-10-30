CREATE DEFINER=`root`@`localhost` PROCEDURE `Z_IMGT_SetCardQuantity`(IN `USERID` int, IN `CARDID` int, IN `CARDQUANTITY` int, IN `CARDTYPE` varchar(10))
BEGIN
SET @total_row = (SELECT COUNT(card_typeid) FROM pangya.pangya_card
WHERE
UID = USERID
AND
card_typeid = CARDID);


CASE
    WHEN TRIM(UPPER(CARDTYPE)) = 'NORMAL' THEN SET CARDTYPE = 0;
    WHEN TRIM(UPPER(CARDTYPE)) = 'RARE' THEN SET CARDTYPE = 1;
    WHEN TRIM(UPPER(CARDTYPE)) = 'SUPER RARE' THEN SET CARDTYPE = 2;
    WHEN TRIM(UPPER(CARDTYPE)) = 'SECRET' THEN SET CARDTYPE = 3;
    ELSE SET CARDTYPE = 0;
END CASE;

# If card_typeid is already exists in table, update otherwise insert new data

IF @total_row >= 1 THEN
	UPDATE pangya.pangya_card SET QNTD = CARDQUANTITY, GET_DT = NOW(), card_type = CARDTYPE WHERE UID = USERID AND card_typeid = CARDID;
END IF;

IF @total_row = 0 THEN
	INSERT INTO pangya.pangya_card (UID,  card_typeid, QNTD, GET_DT, USE_DT, END_DT, Slot, Efeito, Efeito_Qntd, card_type, USE_YN) 
    VALUES
    (USERID, CARDID, CARDQUANTITY, NOW(), NULL, NULL, 0, 0, 0, CARDTYPE, 'N');
END IF;


END