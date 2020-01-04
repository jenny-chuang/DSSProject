USE [TaoyuanActivity]
GO

/****** Object:  StoredProcedure [dbo].[sp_ACT_PREFER]    Script Date: 2019/12/14 �W�� 11:25:22 ******/
DROP PROCEDURE [dbo].[sp_ACT_PREFER]
GO

/****** Object:  StoredProcedure [dbo].[sp_ACT_PREFER]    Script Date: 2019/12/14 �W�� 11:25:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
/**************************************
'�{���N���Gsp_ACT_PREFER
'�{���W�١G��X�U�~�ּh�e�T�Ӭ������O
'�ء@�@���G
'�Ѽƻ����G
            @AgeId VARCHAR, --�~�����O�N��
'�d�@�ҡ@�G

		EXEC dbo.sp_ACT_PREFER '', '2'


**************************************/


CREATE PROC [dbo].[sp_ACT_PREFER]
(
	@AgeId VARCHAR(1),
	@Type  VARCHAR(1)
)
AS 
BEGIN

	SELECT ID, AGE_ID, 
			Keys.x.value('(/n)[1]', 'varchar(16)') AS ACT_TYPE_1,
			Keys.x.value('(/n)[2]', 'varchar(16)') AS ACT_TYPE_2,
			Keys.x.value('(/n)[3]', 'varchar(16)') AS ACT_TYPE_3
	INTO #tmpACT_PRE
	FROM 
	(
		select ID, AGE_ID, convert(xml, '<n>' + replace(ACT_PRE, ',','</n><n>') + '</n>' ) as x 
		FROM ACT_PREFER
	) Keys
	ORDER BY Keys.ID

	SELECT 0 AS ACT_TYPE, '' AS AGE_ID, 0 AS CNT
	INTO #tmpXXX

	DECLARE @_MAX INT, @_i INT
	 SET @_i = 1
	 SET @_MAX = 12 -- �n���ʹX�����

	 WHILE (@_i<= @_MAX)
	 BEGIN
	    INSERT INTO #tmpXXX 
		SELECT @_i AS ACT_TYPE, A.AGE_ID, COUNT(ID) AS CNT
		FROM #tmpACT_PRE A INNER JOIN CODE_TABLE B
			ON A.AGE_ID = B.CODE_ID
		WHERE A.ACT_TYPE_1 = @_i OR A.ACT_TYPE_2 = @_i OR A.ACT_TYPE_3 = @_i
		GROUP BY A.AGE_ID, B.CODE_NAME
		--�[1
		Set @_i=@_i+1
	 END

	 DELETE #tmpXXX WHERE ACT_TYPE = 0

	 SELECT TOP(3) CONVERT(VARCHAR(2), ACT_TYPE) AS ACT_TYPE, AGE_ID, CNT
	 INTO #tmpBBB
	 FROM #tmpXXX 
	 WHERE AGE_ID = @AgeId
	 ORDER BY CNT DESC

	 IF @Type = 1  
		BEGIN
			 SELECT B.CODE_ID, B.CODE_NAME
			 FROM #tmpBBB A INNER JOIN CODE_TABLE B
				ON A.ACT_TYPE = B.CODE_ID
	 END

	 IF @Type = 2  
		BEGIN
			SELECT CODE_ID, CODE_NAME 
			FROM CODE_TABLE
			WHERE CODE_TYPE = 'ACT_TYPE' AND
				  CODE_ID NOT IN (SELECT CODE_ID 
								  FROM #tmpBBB A INNER JOIN CODE_TABLE B
										ON A.ACT_TYPE = B.CODE_ID) 
	END
END
GO


