CREATE DEFINER=`root`@`localhost` PROCEDURE `Z_IMGT_GetAllCards`()
BEGIN
SELECT Nome, card_image.TypeID_Card, Tipo, card_image.TypeID_Pack, card_image.Image, Image_Big FROM pangya.pangya_cards AS card, pangya.z_imgt_card_images AS card_image
WHERE
card.TypeID_Card = card_image.TypeID_Card
AND
card.TypeID_Pack = card_image.TypeID_Pack;
END