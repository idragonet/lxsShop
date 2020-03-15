/*
 Navicat Premium Data Transfer

 Source Server         : lxsshop
 Source Server Type    : SQLite
 Source Server Version : 3030001
 Source Schema         : main

 Target Server Type    : SQLite
 Target Server Version : 3030001
 File Encoding         : 65001

 Date: 13/03/2020 22:03:37
*/

PRAGMA foreign_keys = false;

-- ----------------------------
-- Table structure for goods_cats
-- ----------------------------
DROP TABLE IF EXISTS "goods_cats";
CREATE TABLE "goods_cats" (
  "catId" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
  "parentId" INTEGER,
  "catName" TEXT(20),
  "isShow" INTEGER NOT NULL,
  "catSort" INTEGER NOT NULL,
  "dataFlag" INTEGER,
  "CreateDate" DATETIME DEFAULT (datetime(current_timestamp, 'localtime'))
);

-- ----------------------------
-- Records of goods_cats
-- ----------------------------
INSERT INTO "goods_cats" VALUES (1, NULL, '无尘服', 1, 100, NULL, '2020-03-12 19:31:52');
INSERT INTO "goods_cats" VALUES (2, NULL, '防静电服', 1, 90, NULL, '2020-03-12 21:09:24');
INSERT INTO "goods_cats" VALUES (3, NULL, '防静电鞋', 1, 1, NULL, '2020-03-12 22:07:54');
INSERT INTO "goods_cats" VALUES (4, 1, '百级无尘服', 1, 1, NULL, '2020-03-12 19:38:13');
INSERT INTO "goods_cats" VALUES (5, 1, '千级无尘服', 1, 2, NULL, '2020-03-12 20:38:19');
INSERT INTO "goods_cats" VALUES (11, 2, '防静电分体服', 1, 1, NULL, '2020-03-12 21:09:57');
INSERT INTO "goods_cats" VALUES (12, 2, '防静电大褂', 1, 1, NULL, '2020-03-12 21:10:08');
INSERT INTO "goods_cats" VALUES (13, 2, '防静电连体服', 1, 1, NULL, '2020-03-12 21:09:42');
INSERT INTO "goods_cats" VALUES (14, 3, 'pvc防静电鞋', 1, 1, NULL, '2020-03-12 22:08:11');
INSERT INTO "goods_cats" VALUES (16, 3, '白帆布防静电鞋', 1, 1, NULL, '2020-03-12 22:08:29');
INSERT INTO "goods_cats" VALUES (17, 3, 'c防静电鞋', 1, 1, NULL, '2020-03-13 14:02:09');
INSERT INTO "goods_cats" VALUES (18, NULL, '口罩', 1, 99, NULL, '2020-03-13 21:54:51.4455931');
INSERT INTO "goods_cats" VALUES (19, 18, 'N95', 1, 11, NULL, '2020-03-13 21:56:56.6748683');
INSERT INTO "goods_cats" VALUES (20, 18, 'N90', 1, 1, NULL, '2020-03-13 21:57:58');
INSERT INTO "goods_cats" VALUES (21, 18, 'N97', 1, 1, NULL, '2020-03-13 22:02:51');

-- ----------------------------
-- Auto increment value for goods_cats
-- ----------------------------
UPDATE "sqlite_sequence" SET seq = 21 WHERE name = 'goods_cats';

PRAGMA foreign_keys = true;
