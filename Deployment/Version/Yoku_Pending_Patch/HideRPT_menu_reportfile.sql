--select * from tmenu where ReportFile like '%rpt'

update tmenu set IsActive = 0, IsVisible = 0 where ReportFile like '%rpt'

--select * from tmenu where name like '%filing ty%'
update tmenu set IsActive = 0, IsVisible = 0 where id = 924

GO
UPDATE tMenuTabField SET ShowInBrowser = 1, ShowInList = 1, ShowInInfo = 0 WHERE Name ='ID'
go
UPDATE tMenuTabField SET ShowInList = 0 WHERE name like '%Comment%'

UPDATE tMenuTabField SET ShowInList = 0 WHERE name like '%ApprovalHistory%'

UPDATE tMenuTabField SET ShowInList = 0 WHERE label like '%Approver Status%'

UPDATE tMenuTabField SET ShowInList = 0 WHERE name like '%Reason%' and ShowInList = 1

update tuser set LogInName = 'ASanMiguel' where id = 225
