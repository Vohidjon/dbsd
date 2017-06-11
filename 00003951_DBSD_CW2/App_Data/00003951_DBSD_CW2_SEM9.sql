/* creating database */
CREATE DATABASE [FlowerShop];
GO

USE  [FlowerShop];
GO

/* create query for creating table of customers*/
CREATE TABLE customer (
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	full_name varchar(250) NOT NULL,
	email varchar(250) NOT NULL,
	password varchar(250) NOT NULL
);
GO

/* todo: used for backup */
/*
CREATE TABLE department_backup (
  department_id INT NOT NULL,
  department_name varchar(200) NOT NULL,
  logged_at datetime,
  operation nvarchar(50),
  performer nvarchar(50),
); 
*/

/* create query for creating table of flower categories*/
CREATE TABLE flower_category (
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	name varchar(250) NOT NULL,
	img_url varchar(1000) NOT NULL
);
GO

/* create query for creating table of flowers*/
CREATE TABLE flower (
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	name varchar(250) NOT NULL,
	description varchar(500) NOT NULL,
	img_url varchar(1000) NOT NULL,
	price float NOT NULL,
	remaining INT NOT NULL,
	flower_category_id INT references flower_category(id) NOT NULL
);
GO
  
/* create query for creating table of orders*/
CREATE TABLE flower_order (
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	customer_id INT references customer(id) NOT NULL,
	created_at datetime NOT NULL,
	delivery_address varchar(500) NOT NULL,
	delivery_phone varchar(15),
	process_status int NOT NULL DEFAULT 0
);
GO

/* create query for creating table of order items*/
CREATE TABLE order_item (
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	order_id INT references flower_order(id) NOT NULL,
	flower_id INT references flower(id) NOT NULL,
	quantity INT NOT NULL
);
GO

/* create query for creating table of shopping_cart_item */
CREATE TABLE shopping_cart_item (
  id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  customer_id INT references customer(id) NOT NULL,
  flower_id INT references flower(id) NOT NULL,
  quantity INT NOT NULL
);
GO


/* PROCEDURES & FUNCTIONS */

/* create customer */
CREATE PROCEDURE udpCreateCustomer
              @full_name nvarchar(250),
              @email nvarchar(250),
              @password nvarchar(250)
              AS 
              BEGIN
                INSERT INTO [dbo].[customer]([full_name], [email], [password]) VALUES (@full_name, @email, @password);
              END;
GO

/* check login */
CREATE FUNCTION udfLogin (@email varchar(200), @pass varchar(200))
        RETURNS bit
        AS
        BEGIN
       declare @result bit = 0;
            IF exists(select 1 from dbo.customer where [email] = @email and [password] = @pass) 
            set @result = 1;    

return @result;
        END;

GO
/* create shopping cart item */

CREATE PROCEDURE udpCreateShoppingCartItem
              @customer_id int,
              @flower_id int,
              @quantity int
              AS 
              BEGIN
                INSERT INTO [dbo].[shopping_cart_item]([customer_id], [flower_id], [quantity]) VALUES (@customer_id, @flower_id, @quantity);
              END;
GO

/* update shopping cart item */
CREATE PROCEDURE udpUpdateShoppingCartItem 
        @id int,
        @quantity int
        AS 
        BEGIN
          UPDATE [dbo].[shopping_cart_item] SET quantity = @quantity
          WHERE [id] = @id;
        END;
GO

/* create flower order */
CREATE PROCEDURE udpCreateOrder
              @customer_id int,
              @created_at datetime,
              @delivery_address nvarchar(250),
              @delivery_phone nvarchar(15),
              @process_status int
              AS 
              BEGIN
                INSERT INTO [dbo].[flower_order]([customer_id], [created_at], [delivery_address], [delivery_phone], [process_status]) 
                VALUES 
                (@customer_id, @created_at, @delivery_address, @delivery_phone, @process_status);
              END;
GO

/* create order item */
CREATE PROCEDURE udpCreateOrderItem
              @order_id int,
              @flower_id int,
              @quantity int
              AS 
              BEGIN
                INSERT INTO [dbo].[order_item]([order_id], [flower_id], [quantity]) 
                VALUES 
                (@order_id, @flower_id, @quantity);
              END;
GO

/*
CREATE FUNCTION udfReport(@vacancy_id int, @recruiter_id int)
  RETURNS TABLE
    AS
        RETURN( 
      SELECT s.stage_id, s.stage_name, v.vacancy_title, count(a.stage_id) as cand_count FROM [dbo].[stage] s
      LEFT JOIN [dbo].[application] a on a.stage_id = s.stage_id
      JOIN [dbo].[vacancy] v on a.vacancy_id = v.vacancy_id
      WHERE a.vacancy_id = @vacancy_id AND v.recruiter_id = @recruiter_id AND a.is_disqualified = 0
      GROUP BY s.stage_id, s.stage_name, v.vacancy_title
        );

		*/


/* INSERT SAMPLE DATA */

/*inserting data into customer table*/
INSERT INTO customer (full_name, email, password)
VALUES
('Richard Harley', 'customer1@gmail.com', 'c1'),
('Monica Reculle', 'customer2@gmail.com', 'c2'),
('Durol Keley', 'customer3@gmail.com', 'c3'),
('Fellor Sesey', 'customer4@gmail.com', 'c4');


/*Inserting data into flower_category table */
INSERT INTO flower_category (name, img_url) 
VALUES 
('Houseplants', 'houseplants.jpg'),
('Garden Plants & Flowers', 'garden_plants&flowers.jpg'),
('Romantic Flowers', 'romantic_flowers.jpg'),
('Wild Flowers', 'wild_flowers.jpg'),
('Rare Flowers', 'rare_images.jpg');


/*Inserting data into flower table*/
INSERT INTO flower (name, description, img_url, price, remaining, flower_category_id)
VALUES
(
  'African Violet', 
  'African violets are among the easiest to grow flowering houseplants. They bloom year-round with little effort. Choose from hundreds of varieties and forms, some with variegated foliage or ruffled or white-edged blooms. African violet likes warm conditions and filtered sunlight. Avoid getting water on the fuzzy leaves; cold water causes unsightly brown spots.',
  'african_violet.jpg',
  17.50,
  174,
  1
),
(
  'Hibiscus', 
  'Tropical hibiscus is the ultimate plant for creating a touch of the tropics. It forms huge blooms, up to 8 inches in diameter, on a shrubby upright plant that you can train to grow as a tree. Individual blossoms last only a day or two, but plants bloom freely from late spring through fall and occasionally through winter. Keep the soil uniformly moist and give the plant as much indoor light as possible to keep it blooming.',
  'hibiscus.jpg',
  99.99,
  140,
  1
),
(
  'Flowering Maple', 
  'Crepe-paper-like blooms in shades of red, pink, orange, or yellow dangle among leaves like festive lanterns. Many varieties have splotched or variegated foliage for extra interest. Grow the plant upright as a tree, prune it back to keep it shrubby, or even grow it in a hanging basket. Its common name comes from the leaves, which resemble those of a maple tree.',
  'flowering_maple.jpg',
  19.99,
  100,
  1
),
(
  'Oxalis', 
  'Oxalis bears triangular, clover-like purple leaves and an almost constant show of pink or white blooms. Look for varieties that have plain green foliage with or without silvery accents. Oxalis grows from small bulbils in the soil; you can divide these any time the plant becomes crowded in its pot.',
  'oxalis.jpg',
  98.99,
  113,
  1
),
(
  'Peace Lily', 
  'Peace lily is an easy-care plant that tolerates low light and low humidity. Flowers consist of a showy spoon-shape white spathe and spike of creamy white flowers. Bloom is heaviest in summer, but many varieties bloom throughout the year. The glossy, lance-shape leaves are attractive even when the plant has no blooms',
  'Peace_Lily.jpg',
  49.99,
  47,
  1
),
(
  'Anthurium', 
  'Anthuriums bloom in festive shades of pink, red, lavender, or white, and last for two months or more. They also make a long-lasting cut flower if you can bear to cut them. Anthurium needs medium to bright light to bloom well, but can be grown as a foliage plant with less light.',
  'Anthurium.jpg',
  9.99,
  64,
  1
),
(
  'Jasmine', 
  'There are many types of jasmine. Many-flowered jasmine (J. polyanthum, pictured), and Arabian jasmine (J. sambac) are two of the easiest to grow; just give them plenty of light and moisture. They will all bear fragrant pink to white blooms on vining plants',
  'Jasmine.jpg',
  29.99,
  47,
  1
),
(
  'Kaffir Lily', 
  'Kaffir lily is also commonly called clivia. As a houseplant it usually blooms in winter with clusters of up to 20 reddish orange or yellow tubular flowers. Clivia blooms only when it has been exposed to cool, dry conditions, so give it lower temperatures in winter and keep it on the dry side. With its deep green straplike leaves aligned in a single plane, the plant is attractive even when not in bloom.',
  'KaffirLily.jpg',
  9.99,
  10,
  1
),
(
  'Medium Lavender Hidcote Evergreen Shrub', 
  'Beautiful deep purple flowers top tall stems on silvery evergreen foliage all Summer long. A great lavender to use for containers, low hedging or edging a border',
  'LavenderHidcote.jpg',
  6.80,
  15,
  2
),
(
  'Fuchsia Mix Shrub - 2L', 
  'A great choice for colour from early summer to the first frosts. Great colour and performance year after year. Good for a flowering hedge, large container planting as well as borders. Easy to grow',
  'FuchsiaMixShrub.jpg',
  78,
  10,
  2
),
(
  'Blue Hydrangea', 
  'Blue groups of flowers are borne over delicate ball-like buds on this low maintenance Hydrangea shrub all summer long',
  'BlueHydrangea.jpg',
  10.7,
  33,
  2
),
(
  'Pink Hydrangea', 
  'Pink groups of flowers are borne over delicate ball-like buds on this low maintenance Hydrangea shrub all summer long',
  'PinkHydrangea.jpg',
  17.8,
  57,
  2
),
(
  'Climbing French Bean', 
  'Tasty green beans that are delicious fresh in salads with long pods. Regular picking ensures a long harvesting period',
  'ClimbingFrenchBean.jpg',
  35.25,
  24,
  2
),
(
  'EvergreenBorder', 
  'Evergreen Border Collection creates a low maintenance garden using a variety of evergreen shrubs and perennials, with varying leaf colours and textures. ',
  'EvergreenBorder.jpg',
  47.25,
  47,
  2
),
(
  'Flamingo Dwarf Willow', 
  'A stunning statement plant, ideal for small gardens or patios. A fountain shaped small tree with mottled white pink and green foliage',
  'FlamingoDwarfWillow.jpg',
  49.24,
  96,
  2
),
(
  'Large Monterey Cypress Goldcrest', 
  'Striking, bright, golden-yellow foliage perfect to add shape and colour to any garden or patio. An evergreen tree with unusual scaly bark and aromatic foliage',
  'MontereyCypressGoldcrest.jpg',
  12,
  5,
  2
),
(
  'Rose & Lily Celebration', 
  'Send flowers to celebrate every occasion! The beautiful Rose & Lily Celebration floral bouquet is a stunning gift to send for a birthday, anniversary or to say get well',
  'Rose&LilyCelebration.jpg',
  14,
  58,
  3
),
(
  'One Dozen Long Stemmed Red Roses', 
  'Celebrate your love for a special someone with a classic bouquet of red roses. Red roses carry the deep meaning of love and affection, the perfect symbol of how much someone means to you today. ',
  'LongStemmedRedRoses.png',
  74,
  46,
  3
),
(
  'Ruby Rose Bouquet', 
  'Make someone blush with a delivery of the Ruby Rose Bouquet! This romantic arrangement is overflowing with fresh red roses, blue iris and purple statice',
  'RubyRoseBouquet.jpg',
  34,
  24,
  3
),
(
  'Two Dozen Long Stemmed Red Roses', 
  'Wow them with roses! A fresh bouquet of red roses is sure to create a lasting memory. Fresh long-stemmed roses are a beautiful gift for delivery.',
  'TwoDozenLongStemmedRedRoses.jpg',
  35.87,
  83,
  3
),
(
  'One Dozen Red and Pink Roses', 
  'Set apart from the traditional single color rose bouquet, this color combination of red and pink comes together to form a symbolic blend of romance and passion.',
  'OneDozenRedandPinkRoses.png',
  8.99,
  37,
  3
),
(
  'One Dozen Rainbow Roses', 
  'Fill their day with color with a bouquet of mixed roses. A beautiful classic choice, mixed roses are the perfect gift to send to a friend or a loved one',
  'OneDozenRainbowRoses.jpg',
  36.69,
  12,
  3
),
(
  'You are In My Heart', 
  'Let your special someone know that they are in your heart, with a hand delivery of red flowers. The You are In My Heart bouquet is created with fresh red roses and red carnations. ',
  'YouAreInMyHeart.jpg',
  46.74,
  24,
  3
),
(
  'Roses with Godiva Chocolates & Bear', 
  'Create a moment to treasure by sending our velvety red roses. This garden-fresh bouquet arrives with a box of chocolates and a cuddly teddy bear',
  'RoseswithGodivaChocolates&Bear.jpg',
  35.99,
  13,
  3
),
(
  'Bell Heather', 
  'Bell Heather is found in a variety of harsh habitats including heathland, acidic soils, open woodland and even coastal areas. It particularly likes dry, well-drained soils.',
  'BellHeather.JPG',
  17.99,
  17,
  4
),
(
  'Bluebell', 
  'Bluebells spend most of the year as bulbs underground in ancient woodlands, only emerging to flower and leaf from April onwards. This early spring flowering allows them to make the most of the sunlight that is still able to make it to their forest floor habitat and attracts the attention of plenty of pollinating insects.',
  'Bluebell.JPG',
  61.99,
  4,
  4
),
(
  'Bulbous buttercup', 
  'Bulbous buttercup is a native perennial herb, which gets its name from its distinctive perennating organ, a bulb-like swollen underground stem, which is situated just below the soil surface.',
  'Bulbousbuttercup.jpg',
  43.99,
  23,
  4
),
(
  'Butterwort', 
  'An insectivorous plant, the bright yellow-green leaves of Common Butterwort excrete a sticky fluid which attracts unsuspecting insects. Once an insect get trapped, the leaves slowly curl around their prey and digest it.',
  'Butterwort.JPG',
  63.99,
  9,
  4
),
(
  'Kadupul flower', 
  'Rareness and beauty are main things that make kadupul flower so special. This beautiful flower mainly found in forests of Sri Lanka.',
  'Kadupulflower.jpg',
  42.99,
  12,
  5
),
(
  'Campion', 
  'Campion or silene tomentosa can only be found in the British territory called Gibraltar. It’s a weak fragrant, evening blooming flower with a short life span. Interestingly, in 1992, the botanical section of Gibraltar officially declared that there are no campion flowers left and became extinct.',
  'Campion.jpg',
  100.99,
  71,
  5
),
(
  'Ghost Orchid', 
  'Ghost orchid is a rare, spider web like flower that grows in Cuba and Florida. Apart from its natural environment, it seems very difficult to cultivate ghost orchid. It needs high temperature and high humidity.',
  'GhostOrchid.jpg',
  42.99,
  12,
  5
);



/*Inserting data into flower_order table*/
INSERT INTO flower_order (customer_id, created_at, delivery_address, delivery_phone, process_status)
VALUES
(
  1, 
  '2017-01-01 15:10:47', 
  '8400 London Place Washington, DC 20521-8400',
  null,
  1
),
(
  1,
  '2017-02-04 12:13:07', 
  '7100 Athens Place Washington, DC 20521-7100',
  '+1-541-754-3010',
  1
),
(
  2, 
  '2016-07-01 15:10:00', 
  '5520 Quebec Place Washington, DC 20521-5520',
  null,
  2
),
(
  2,
  '2017-02-04 12:13:07', 
  '7100 Athens Place Washington, DC 20521-7100',
  '+6-188-232-6262',
  2
);


/*Inserting data into order_item table*/
INSERT INTO order_item (order_id, flower_id, quantity)
VALUES
(1, 1, 3),
(1, 3, 1),
(1, 6, 1),
(2, 7, 1),
(2, 5, 2),
(3, 13, 2),
(3, 15, 1),
(3, 17, 1),
(4, 16, 2),
(4, 12, 1);
