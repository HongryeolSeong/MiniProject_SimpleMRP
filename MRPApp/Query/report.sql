-- 1. PrcResult에서 성공갯수와 실패갯수를 다른 (가상)컬럼으로 분리하여 가상 테이블 만듦
SELECT p.SchIdx , p.PrcDate, 
       CASE p.PrcResult WHEN 1 THEN 1 ELSE 0 END AS PrcOK,
	   CASE p.PrcResult WHEN 0 THEN 1 ELSE 0 END AS PrcFail
  FROM Process As p

-- 2. 합계 집계 결과 역시 가상 테이블
SELECT smr.SchIdx, smr.PrcDate, SUM(smr.PrcOK) AS PrcOkAmount, SUM(smr.PrcFail) AS PrcFailAmount
  FROM (
		SELECT p.SchIdx , p.PrcDate, 
          CASE p.PrcResult WHEN 1 THEN 1 ELSE 0 END AS PrcOK,
	      CASE p.PrcResult WHEN 0 THEN 1 ELSE 0 END AS PrcFail
          FROM Process As p
	   ) AS smr
 GROUP BY smr.SchIdx, smr.PrcDate

 -- 3.0 조인문
 SELECT * 
   FROM Schedules AS sch 
  INNER JOIN Process AS prc
     ON sch.SchIdx = prc.SchIdx

 -- 3.1 2번 결과(가상테이블)와 Schedules 테이블 조인해서 원하는 결과 도출
 SELECT sch.SchIdx, sch.PlantCode, sch.SchAmount, prc.PrcDate, prc.PrcOkAmount, prc.PrcFailAmount
   FROM Schedules AS sch
  INNER JOIN (
				  SELECT smr.SchIdx, smr.PrcDate, SUM(smr.PrcOK) AS PrcOkAmount, SUM(smr.PrcFail) AS PrcFailAmount
				    FROM (
					    	SELECT p.SchIdx , p.PrcDate, 
						    CASE p.PrcResult WHEN 1 THEN 1 ELSE 0 END AS PrcOK,
						    CASE p.PrcResult WHEN 0 THEN 1 ELSE 0 END AS PrcFail
						    FROM Process As p
					     ) AS smr
				   GROUP BY smr.SchIdx, smr.PrcDate
  ) AS prc
    ON sch.SchIdx = prc.SchIdx