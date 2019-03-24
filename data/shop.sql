/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 100138
 Source Host           : localhost:3306
 Source Schema         : shop

 Target Server Type    : MySQL
 Target Server Version : 100138
 File Encoding         : 65001

 Date: 24/03/2019 21:36:10
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for categories
-- ----------------------------
DROP TABLE IF EXISTS `categories`;
CREATE TABLE `categories`  (
  `category_id` int(11) NOT NULL AUTO_INCREMENT,
  `category_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  PRIMARY KEY (`category_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 12 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of categories
-- ----------------------------
INSERT INTO `categories` VALUES (1, 'Xe Leo Núi');
INSERT INTO `categories` VALUES (2, 'Xe Thành Phố');
INSERT INTO `categories` VALUES (3, 'Xe Trẻ Em');
INSERT INTO `categories` VALUES (4, 'Phụ Kiện');
INSERT INTO `categories` VALUES (11, 'Xe hơi');

-- ----------------------------
-- Table structure for customers
-- ----------------------------
DROP TABLE IF EXISTS `customers`;
CREATE TABLE `customers`  (
  `cus_id` int(20) NOT NULL AUTO_INCREMENT,
  `cus_name` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `cus_password` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `cus_fullname` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `cus_email` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `cus_phone` varchar(15) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`cus_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of customers
-- ----------------------------
INSERT INTO `customers` VALUES (0, 'admin', '123456', '0', '0', '');
INSERT INTO `customers` VALUES (1, 'aaaa', 'aaaa', 'aaaa', 'aaaa', 'aaaa');
INSERT INTO `customers` VALUES (2, 'aaaaaa', 'aaaaaa', 'aaaaaa', 'aaaaaa', 'aaaaaa');
INSERT INTO `customers` VALUES (3, 'aaaaaaaaa', 'aaaaaaaa', 'aaaaaaaaa', 'aaaaaaaa', 'aaaaaaaa');

-- ----------------------------
-- Table structure for order_details
-- ----------------------------
DROP TABLE IF EXISTS `order_details`;
CREATE TABLE `order_details`  (
  `order_detail_id` int(11) NOT NULL AUTO_INCREMENT,
  `order_id` int(11) NULL DEFAULT NULL,
  `product_id` int(11) NULL DEFAULT NULL,
  `price` int(50) NULL DEFAULT NULL,
  `quantity` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`order_detail_id`) USING BTREE,
  INDEX `bill_pk`(`order_id`) USING BTREE,
  INDEX `product_pk`(`product_id`) USING BTREE,
  CONSTRAINT `bill_pk` FOREIGN KEY (`order_id`) REFERENCES `orders` (`order_id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `product_pk` FOREIGN KEY (`product_id`) REFERENCES `products` (`product_id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_bin ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for orders
-- ----------------------------
DROP TABLE IF EXISTS `orders`;
CREATE TABLE `orders`  (
  `order_id` int(11) NOT NULL AUTO_INCREMENT,
  `customer_id` int(11) NULL DEFAULT NULL,
  `payment` int(1) NULL DEFAULT NULL,
  `address` varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `date` timestamp(0) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(0),
  `status` int(1) NOT NULL,
  PRIMARY KEY (`order_id`) USING BTREE,
  INDEX `customer_pk`(`customer_id`) USING BTREE,
  CONSTRAINT `customer_pk` FOREIGN KEY (`customer_id`) REFERENCES `customers` (`cus_id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of orders
-- ----------------------------
INSERT INTO `orders` VALUES (1, 1, 1, '123 Tran Xuan Soan', '2019-03-23 00:00:00', 1);
INSERT INTO `orders` VALUES (2, 1, 1, '123 NTMK', '2019-03-23 11:02:17', 1);

-- ----------------------------
-- Table structure for products
-- ----------------------------
DROP TABLE IF EXISTS `products`;
CREATE TABLE `products`  (
  `product_id` int(11) NOT NULL AUTO_INCREMENT,
  `category_id` int(11) NOT NULL,
  `provider_id` int(11) NOT NULL,
  `product_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `product_image` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `product_price` int(50) NOT NULL,
  `quantities` int(11) NOT NULL,
  `product_description` longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL,
  PRIMARY KEY (`product_id`) USING BTREE,
  INDEX `provider_pk`(`provider_id`) USING BTREE,
  INDEX `cate_pk`(`category_id`) USING BTREE,
  CONSTRAINT `cate_pk` FOREIGN KEY (`category_id`) REFERENCES `categories` (`category_id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `provider_pk` FOREIGN KEY (`provider_id`) REFERENCES `providers` (`provider_id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 25 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of products
-- ----------------------------
INSERT INTO `products` VALUES (4, 11, 1, 'Flyte Sport', '/Content/Uploads/Flyte.png', 4500000, 50, 'Dòng Flyte là dòng xe đạp địa hình cơ bản cho các bạn lần đầu làm quen với loại bánh có kích thước lớn.\r\nĐược trang bị khung xe bằng thép carbon hoặc nhôm cùng phuộc nhún, tay đề nhiều tốc độ và thắng đĩa, dù bạn đạp xe trong thành phố hay những con đường mòn, dòng xe đạp Flyte sẽ là sự kết hợp hoàn hảo đưa bạn đến với niềm vui của những vòng quay xe đạp và tốc độ.\r\n');
INSERT INTO `products` VALUES (5, 1, 1, 'Nitro Comp', '/Content/Uploads/Nitro.png', 4200000, 50, 'Dòng Nitro được thiết kế cho người mới bắt đầu sử dụng xe đạp địa hình (MTB).\r\nTrang bị khung thép-carbon hoặc khung nhôm bên cạnh phuộc nhún chống xóc, tay đề gạt nhiều tốc độ, thắng đĩa hoặc thắng chữ V và yên xe êm ái. Dù chạy trong thành phố hay đường mòn, Nitro vẫn là sự lựa chọn phù hợp và luôn thể hiện tốt nhiệm vụ bạn giao cho.\r\n');
INSERT INTO `products` VALUES (6, 1, 1, 'Viper DS', '/Content/Uploads/ViperDS.png', 4900000, 50, 'Xe đạp mang nhãn hiệu Viper được phát triển từ dòng sản phẩm anh em Nitro.\r\nKế thừa những tính năng của một chiếc xe đạp địa hình (MTB) với cấu trúc hình học tiêu chuẩn, bộ khung sườn rắn chắc, thắng đĩa và phuộc nhún nhưng được thiết kế lại với kích thước nhỏ hơn.\r\n');
INSERT INTO `products` VALUES (7, 1, 1, 'Duke 9', '/Content/Uploads/Duke.png', 9900000, 50, 'The Duke, loại xe bánh lớn 4” có thể được xem là một “sát thủ” trên các con đường đất cát, sình lầy nhờ vào phần bánh rộng với áp suất thấp.\r\nDòng the Duke với các tính năng như khung sườn bằng thép-carbon hoặc nhôm, phuộc cứng hoặc phuộc nhún, được trang bị 1 hoặc nhiều tốc độ sẽ giúp bạn vượt qua các địa hình mà trước đây bạn chưa từng nghĩ đến.');
INSERT INTO `products` VALUES (8, 2, 1, 'Savannah', '/Content/Uploads/Savannah.png', 4200000, 50, 'Phong cách độc đáo và đầy đủ các tính năng mà một chiếc xe đạp cần có với 1 tốc độ và bạn chỉ cần có thế thì đây là chiếc xe đạp dành cho bạn.\r\nCấu trúc hình học được thiết kế tạo sự thoải mái và phù hợp hơn, rổ xe, vè xe, yên sau là những phụ kiện kèm theo bên cạnh màu sắc thời trang cùng khung xe độc đáo hoàn toàn mới giúp bạn luôn nổi bật.');
INSERT INTO `products` VALUES (9, 2, 1, 'Strada Comp', '/Content/Uploads/Strada.png', 4200000, 50, 'Với trọng lượng nhẹ, khung sườn thiết kế tối ưu theo khí động học và cấu trúc hình học tiêu chuẩn cho dòng xe thành phố.\r\nStrada là cỗ máy trong mơ cho những con đường thành thị. Dòng xe có nhiều sự lựa chọn cho bạn từ loại có vè xe, yên sau, phuộc nhún hay phuộc cứng cùng các tùy chọn khác như thắng và bộ truyền động.\r\n');
INSERT INTO `products` VALUES (10, 2, 1, 'Catina', '/Content/Uploads/Catina.png', 3500000, 50, 'Mang phong cách thành thị hiện đại, chiếc xe đạp thành phố Catina kế thừa những nét cổ điển vốn có với thiết kế hiện đại hơn. \r\nJett hiểu rằng những con dốc hay đoạn đường đèo sẽ gây cản trở cho bạn và Catina sẽ tạo nên điểm khác biệt khi trang bị bộ truyền động nhiều tốc độ của Shimano giúp bạn vượt qua một cách dễ dàng.\r\nCấu trúc hình học được thiết kế tạo sự thoải mái và phù hợp hơn, rổ xe, vè xe, yên sau và chuông là những phụ kiện kèm theo bên cạnh các màu sắc thời trang với những họa tiết tạo điểm nhấn giúp bạn luôn nổi bật. ');
INSERT INTO `products` VALUES (11, 2, 1, 'Daily', '/Content/Uploads/Daily.png', 2500000, 50, 'Phong cách độc đáo và đầy đủ các tính năng mà một chiếc xe đạp cần có với 1 tốc độ và bạn chỉ cần có thế thì đây là chiếc xe đạp dành cho bạn. Cấu trúc hình học được thiết kế tạo sự thoải mái và phù hợp hơn, rổ xe, vè xe, yên sau và chuông là những phụ kiện kèm theo bên cạnh màu sắc thời trang cùng những họa tiết tạo điểm nhấn giúp bạn luôn nổi bật.\r\n');
INSERT INTO `products` VALUES (12, 2, 1, 'Kinetic', '/Content/Uploads/Kinetic.png', 5400000, 50, 'Dòng Kinetic được thiết kế để khắc phục những hạn chế vốn có của nhà ở đô thị hiện nay.\r\nBánh xe nhỏ hơn cho phép chiều dài cơ sở ngắn hơn, nhưng với khung xe được gia công kéo dài giúp người lái vẫn giữ được tư thế thoải mái như khi lái xe bánh lớn. Cùng mục đích ấy, tỷ lệ bánh răng giúp giữ tốc độ chạy nhanh hơn, và giờ không còn điều gì có thể gây cản trở bạn.\r\n');
INSERT INTO `products` VALUES (13, 3, 1, 'Raider', '/Content/Uploads/Raider.png', 2900000, 50, 'Xe đạp đã trở thành một phần trong quá trình phát triển của trẻ, cũng như giai đoạn phải thay răng khi lớn lên.\r\nDòng xe đạp trẻ em của Jett Cycles được phát triển từ chiếc xe giữ thăng bằng cho bé chập chững biết đi cho đến những chiếc xe dành cho các tay đua nhí với kích thước bánh từ 16 inches đến 20 inches, trang bị 1 tốc độ hoặc nhiều tốc độ. Và với Jett, thật tuyệt khi là trẻ em');
INSERT INTO `products` VALUES (14, 3, 1, 'Striker', '/Content/Uploads/Striker.png', 3900000, 50, 'Xe đạp đã trở thành một phần trong quá trình phát triển của trẻ, cũng như giai đoạn phải thay răng khi lớn lên.\r\nDòng xe đạp trẻ em của Jett Cycles được phát triển từ chiếc xe giữ thăng bằng cho bé chập chững biết đi cho đến những chiếc xe dành cho các tay đua nhí với kích thước bánh từ 16 inches đến 20 inches, trang bị 1 tốc độ hoặc nhiều tốc độ. Và với Jett, thật tuyệt khi là trẻ em');
INSERT INTO `products` VALUES (15, 3, 1, 'Bronx', '/Content/Uploads/BMX.png', 140000, 50, 'Đường phố, công viên, đường mòn, đường ray và dốc, những địa điểm yêu thích của bộ môn BMX và là nơi giúp bạn tự tin thể hiện bản thân.\r\nKhung sườn từ thép carbon, tích hợp ống đầu, ống trục giữa, giò đạp hình ống và những phụ tùng khác với công nghệ tiên tiến và mới nhất.\r\n');
INSERT INTO `products` VALUES (16, 4, 1, 'Tool', '/Content/Uploads/Tool.png', 390000, 50, 'Bác sĩ cho những cuộc hành trình.Với chất liệu thép carbon cứng chắc, được gia công bằng máy với dung sai một cách chính xác và được phủ một lớp bảo vệ giúp gia tăng độ bền.');
INSERT INTO `products` VALUES (17, 4, 1, 'Bottle', '/Content/Uploads/Bottle.png', 120000, 50, 'Đạp xe dưới thời tiết nắng nóng và bạn dường như cảm thấy luôn thiếu nước. Bình nước với đầu vòi lớn sẽ cung cấp một lượng nước lớn hơn và giúp bạn giải khát nhanh hơn.');
INSERT INTO `products` VALUES (18, 4, 1, 'Cage', '/Content/Uploads/Cage.png', 75000, 50, 'Được sản xuất từ loại nhôm chuyên dụng cho phi cơ với các đặc tính chắc chắn và nhẹ, dễ dàng đánh bóng sau một thời gian sử dụng. Đa dạng màu sắc để lựa chọn cho chiếc xe của bạn.');
INSERT INTO `products` VALUES (19, 4, 1, 'Computer', '/Content/Uploads/Computer.png', 250000, 50, 'Không dây, không vướng bận.Sẽ không có cách nào tốt hơn để đo thành tích đạp xe của bạn bằng việc sử dụng đồng hồ đo tốc độ Jett 8ight.\r\nChế độ hiển thị toàn màn hình độc đáo, phiên bản nâng cấp với công nghệ không dây và 8 chức năng cung cấp cho các thông tin chính xác nhất sau mỗi ngày đạp');
INSERT INTO `products` VALUES (20, 4, 1, 'Fender', '/Content/Uploads/Fender.png', 290000, 50, 'Bảo vệ bạn trong những ngày mưa.');
INSERT INTO `products` VALUES (21, 4, 1, 'Gelcover', '/Content/Uploads/Gelcover.png', 190000, 50, 'Tấm đệm lót yên Jett sử dụng loại gel mật độ cao được thiết kế với hệ thống rãnh ở giữa giúp hạn chế tối đa áp lực khi ngồi. ');
INSERT INTO `products` VALUES (22, 4, 1, 'Bell', '/Content/Uploads/Icon.png', 120000, 50, 'Chuông xe Jett giúp bạn cảnh báo người đi xe máy , hay gọi ai đó một cách vui nhộn. Hay đơn giản là bạn muốn thêm cá tính cho chiếc xe của mình.');
INSERT INTO `products` VALUES (23, 4, 1, 'Kickstand', '/Content/Uploads/Kickstand.png', 250000, 50, 'Hãy nghỉ ngơi. Cứ để em ấy lo');
INSERT INTO `products` VALUES (24, 4, 1, 'Light', '/Content/Uploads/Light.png', 140000, 50, 'Chiếu sáng cả đoạn đường');

-- ----------------------------
-- Table structure for providers
-- ----------------------------
DROP TABLE IF EXISTS `providers`;
CREATE TABLE `providers`  (
  `provider_id` int(20) NOT NULL AUTO_INCREMENT,
  `provider_name` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `provider_email` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `provider_address` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `provider_phone` varchar(15) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`provider_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of providers
-- ----------------------------
INSERT INTO `providers` VALUES (1, 'FPT', 'support@fpt.com.vn', '12 Tầm Bậy', '0938965851');

-- ----------------------------
-- Table structure for users
-- ----------------------------
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users`  (
  `user_id` int(20) NOT NULL AUTO_INCREMENT,
  `username` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `password` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `fullname` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `email` varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`user_id`) USING BTREE,
  UNIQUE INDEX `username`(`username`, `email`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 15 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of users
-- ----------------------------
INSERT INTO `users` VALUES (13, 'aaaaa', 'aaaaa', 'test', 'vinhduy111@gmail.com');
INSERT INTO `users` VALUES (14, 'admin', 'admin', 'admin', 'vinhduy11@gmail.com');

-- ----------------------------
-- Procedure structure for GetAllOrders
-- ----------------------------
DROP PROCEDURE IF EXISTS `GetAllOrders`;
delimiter ;;
CREATE PROCEDURE `GetAllOrders`()
BEGIN
   
	SELECT
	o.order_id,
	o.customer_id,
	c.cus_name,
	b.total,
	o.payment,
	o.address,
	o.date,
	o.STATUS status
FROM
	orders o
	JOIN ( SELECT od.order_id, sum( od.quantity * od.price ) total FROM order_details od GROUP BY od.order_id ) b ON b.order_id = o.order_id
JOIN customers c on c.cus_id = o.order_id;

END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for GetOrderById
-- ----------------------------
DROP PROCEDURE IF EXISTS `GetOrderById`;
delimiter ;;
CREATE PROCEDURE `GetOrderById`(IN `id` int)
BEGIN
   
	SELECT
	o.order_id,
	o.customer_id,
	c.cus_name,
	b.total,
	o.payment,
	o.address,
	o.date,
	o.STATUS status
FROM
	orders o
	JOIN ( SELECT od.order_id, sum( od.quantity * od.price ) total FROM order_details od GROUP BY od.order_id ) b ON b.order_id = o.order_id
JOIN customers c on c.cus_id = o.order_id
where o.order_id = @id;

END
;;
delimiter ;

SET FOREIGN_KEY_CHECKS = 1;
