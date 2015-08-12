接口列表
==================================
> user
>> notebook - 查看用户全部笔记本  
>> create - 创建用户  
>> update - 更新用户信息  
>
> notebook  
>> all - 查看用户全部笔记本（同user/notebook）  
>> get - 查看笔记本信息  
>> list - 列出笔记本下的笔记  
>> update - 修改笔记本   
>> create - 创建笔记本  
>> delete - 删除笔记本  
>
> note  
>> create - 创建笔记  
>> get - 查看笔记  
>> update - 修改笔记  
>> move - 移动笔记  
>> delete - 删除笔记  
>

数据结构
==================================

user｛
    id,
    nickname,
    [pass],
    notebook_list
｝

notebook｛
    id,
    [pass],
    name,
    notes_num,
    note_list
｝

note ｛
    id,
    name,
    notebook_id,
    type(0),
    data
｝

接口输入输出
==================================

user/notebook
------------------
input｛
    user-nickname,
    user-pass
｝

output｛
    notebook list
｝

user/create
------------------
input｛
    user-nickname,
    user-pass
｝

output｛
    ok
｝

user/update
------------------
input｛
    user-nickname,
    user-pass,
    new nickname，
    new pass
｝

output｛
    ok
｝

notebook/all
------------------
input｛
    user-nickname,
    user-pass
｝

output｛
    notebook list
｝

notebook/get
------------------
input｛
    notebook-id,
    notebook-pass
｝

output｛
    notebook-name,
    notebook-notes_num
｝

notebook/list
------------------
input｛
    notebook-id,
    notebook-pass
｝

output｛
    note list
｝

notebook/update
------------------
input｛
    notebook-id,
    notebook-pass,
    new name,
    new pass
｝

output｛
    ok
｝

notebook/create
------------------
input｛
    user-name,
	user-pass,
    notebook-id,
    notebook-pass
｝

output｛
    notebook-id
｝

notebook/delete
------------------
input｛
    notebook-id,
    notebook-pass
｝

output｛
    ok
｝

note/create
------------------
input｛
    notebook-id,
    notebook-pass,
    note-name,
    note-type,
    note-data
｝

output｛
    note-id
｝

note/get
------------------
input｛
    notebook-id,
    notebook-pass,
    note-id
｝

output｛
    note
｝

note/update
------------------
input｛
    notebook-id,
    notebook-pass,
    note-id,
    new name,
    new type,
    new data
｝

output｛
    ok
｝

note/move
------------------
input｛
    from notebook-id,
    from notebook-pass,
    to notebook-id,
    to notebook-pass,
    note-id
｝

output｛
    ok
｝

note/delete
------------------
input｛
    notebook-name,
    notebook-pass,
    note-id
｝

output｛
    ok
｝