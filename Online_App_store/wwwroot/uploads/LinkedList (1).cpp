#include <iostream>
using namespace std;

class Node 
{
public:
    int data;
    Node* pNext;
};

class Linkedlist
{
public:
    Node* pHead;
    Node* pTail;
    Linkedlist() 
    {
        pHead = nullptr;
        pTail = nullptr;
    }
    void insert(Node *pTemp)
    {
        if (pHead == nullptr) 
        {
            pHead = pTemp;
            pTail = pTemp;
        }
        else
        {
            pTail->pNext = pTemp;
            pTail = pTemp;
        }
    }
    void displayAll() 
    {
        Node* pTrav = pHead;
        while (pTrav != nullptr) 
        {
            cout << pTrav->data << " ";
            pTrav = pTrav->pNext;
        }
    }
};


int main()
{
    Linkedlist l;
    int n;
    cout<<"Please enter how many nodes"<<endl;
    cin>>n;
    int v;
    Node *pTemp;
    for (int i = 0; i < n; i++)
    {
        pTemp = new Node();
        cin>>v;
        pTemp->data = v;
        l.insert(pTemp);
    }
    l.displayAll();
    return 0;
}

