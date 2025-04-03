## Вариантр А

### Создание 10 тасок, в каждой из которых устанавливается значение свойства "AsyncLocalString" и спустя какое-то время вычитывается значение из свойства "AsyncLocalString", после чего таска завершается

В AsyncLocalString будет записана строка: 'Value5'  
В AsyncLocalString будет записана строка: 'Value7'  
В AsyncLocalString будет записана строка: 'Value3'  
В AsyncLocalString будет записана строка: 'Value1'  
В AsyncLocalString будет записана строка: 'Value8'  
В AsyncLocalString будет записана строка: 'Value4'  
В AsyncLocalString будет записана строка: 'Value9'  
В AsyncLocalString будет записана строка: 'Value2'  
В AsyncLocalString будет записана строка: 'Value6'  
В AsyncLocalString будет записана строка: 'Value0'  
[13] - Changed - '' => ''Value6''  
[15] - Changed - '' => ''Value8''  
[12] - Changed - '' => ''Value5''  
[11] - Changed - '' => ''Value4''  
[6] - Changed - '' => ''Value0''  
[9] - Changed - '' => ''Value2''  
[16] - Changed - '' => ''Value9''  
Из AsyncLocalString получена строка: ''Value2''  
Из AsyncLocalString получена строка: ''Value9''  
[14] - Changed - '' => ''Value7''  
Из AsyncLocalString получена строка: ''Value6''  
Из AsyncLocalString получена строка: ''Value8''  
Из AsyncLocalString получена строка: ''Value5''  
[12] - Changed - ''Value5'' => '' - Поток вернулся в пул потоков.  
Из AsyncLocalString получена строка: ''Value0''  
[10] - Changed - '' => ''Value3''  
[7] - Changed - '' => ''Value1''  
[16] - Changed - ''Value9'' => '' - Поток вернулся в пул потоков.  
Из AsyncLocalString получена строка: ''Value1''  
[7] - Changed - ''Value1'' => '' - Поток вернулся в пул потоков.  
Из AsyncLocalString получена строка: ''Value7''  
[14] - Changed - ''Value7'' => '' - Поток вернулся в пул потоков.  
[15] - Changed - ''Value8'' => '' - Поток вернулся в пул потоков.  
Из AsyncLocalString получена строка: ''Value4''  
[6] - Changed - ''Value0'' => '' - Поток вернулся в пул потоков.  
Из AsyncLocalString получена строка: ''Value3''  
[10] - Changed - ''Value3'' => '' - Поток вернулся в пул потоков.  
[13] - Changed - ''Value6'' => '' - Поток вернулся в пул потоков.  
[11] - Changed - ''Value4'' => '' - Поток вернулся в пул потоков.  
[9] - Changed - ''Value2'' => '' - Поток вернулся в пул потоков.  


## Вариант B 

### В основном потоке устанавливается, а затем спустя некоторое время меняется значение свойства "AsyncLocalString". В таске выполняется только считывание значения "AsyncLocalString"

[1] - Changed - '' => 'Value 1'  
AsyncMethodB Entering - Expected 'Value 1', AsyncLocal value is 'Value 1'  
AsyncMethodB Exiting - Expected 'Value 1', AsyncLocal value is 'Value 1'  
[1] - Changed - 'Value 1' => 'Value 2'  
[4] - Changed - '' => 'Value 1'  
[4] - SubTask - Expected 'Value 1', AsyncLocal value is 'Value 1'  
[4] - Changed - 'Value 1' => '' - Поток вернулся в пул потоков.  
[1] - Changed - 'Value 2' => '' - Поток вернулся в пул потоков.  
[7] - Changed - '' => 'Value 2'  
[7] - Main - Expected 'Value 2', AsyncLocal value is 'Value 2'  

