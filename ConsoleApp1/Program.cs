using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            MyLinkedList obj = new();
            obj.AddAtHead(5);
            obj.AddAtHead(4);
            obj.AddAtHead(3);
            obj.AddAtHead(2);
            obj.AddAtHead(1);



            PrintLinkList(obj.Head);
            var newHead = obj.ReverseList(obj.Head);
            PrintLinkList(newHead);
            Console.WriteLine("Link list has cycle:- " + obj.DetectCycle(obj.Head));
        }
        public static void PrintLinkList(ListNode Head)
        {
            Console.WriteLine("Now, Array is ");
            ListNode tempNode = Head;
            while (tempNode != null)
            {
                Console.Write(":: " + tempNode.val);
                tempNode = tempNode.next;
            }
        }
    }

    public class MyLinkedList
    {

        public ListNode Head;
        /** Initialize your data structure here. */
        public MyLinkedList()
        {
            Head = null;
        }

        /** Get the value of the index-th node in the linked list. If the index is invalid, return -1. */
        public int Get(int index)
        {
            if (Head == null || index < 0) { return -1; }

            ListNode tempNode = Head;
            while (tempNode != null)
            {
                if (index == 0)
                {
                    return tempNode.val;
                }
                tempNode = tempNode.next;
                index--;
            }
            return -1;
        }

        /** Add a node of value val before the first element of the linked list. After the insertion, the new node will be the first node of the linked list. */
        public void AddAtHead(int val)
        {
            ListNode newNode = new(val);
            if (Head != null)
            {
                newNode.next = Head;
            }

            Head = newNode;
        }

        /** Append a node of value val to the last element of the linked list. */
        public void AddAtTail(int val)
        {
            ListNode newNode = new(val);
            if (Head == null)
            {
                Head = newNode;
            }
            ListNode tempNode = Head;
            while (tempNode.next != null)
            {
                tempNode = tempNode.next;
            }
            tempNode.next = newNode;

        }

        /** Add a node of value val before the index-th node in the linked list. If index equals to the length of linked list, the node will be appended to the end of linked list. If index is greater than the length, the node will not be inserted. */
        public void AddAtIndex(int index, int val)
        {
            ListNode newNode = new(val);

            if (Head == null && index > 0)
            {
                return;
            }
            if (Head == null && index == 0)
            {
                Head = newNode;
                return;
            }
            if (Head != null && index == 0)
            {
                newNode.next = Head;
                Head = newNode;
                return;
            }

            ListNode tempNode = Head;
            while (tempNode.next != null)
            {
                if (index == 1) { break; }
                tempNode = tempNode.next;
                index--;
            }
            if (tempNode != null)
            {
                newNode.next = tempNode.next;
                tempNode.next = newNode;
            }
        }

        /** Delete the index-th node in the linked list, if the index is valid. */
        public void DeleteAtIndex(int index)
        {
            if (Head == null || index < 0) return;

            ListNode tempNode = Head;
            if (index == 0)
            {
                Head = tempNode.next;
                return;
            }
            for (int i = 0; tempNode != null && i < index - 1; i++)
                tempNode = tempNode.next;

            // If position is more than number of nodes
            if (tempNode == null || tempNode.next == null)
                return;

            // Node temp->next is the node to be deleted
            // Store pointer to the next of node to be deleted
            ListNode next = tempNode.next.next;

            // Unlink the deleted node from list
            tempNode.next = next;
        }

        public static bool HasCycle(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return false;
            }

            ListNode firstPointer = head, secondPointer = head.next;


            while (secondPointer != null)
            {
                if (firstPointer.Equals(secondPointer))
                {
                    return true;
                }
                firstPointer = firstPointer.next;
                secondPointer = secondPointer.next?.next;
            }

            return false;
        }
        public ListNode DetectCycle(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return null;
            }

            ListNode firstPointer = head;
            int maxValue = 1000000;
            while (firstPointer != null && firstPointer.next != null)
            {
                if (firstPointer.val > maxValue)
                {
                    return firstPointer;
                }
                firstPointer.val = Math.Abs(firstPointer.val) + maxValue;
                firstPointer = firstPointer.next;
            }

            return null;
        }

        public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
        {

            ListNode pointerA = headA, pointerB = headB;
            int countA = 0, countB = 0;
            while (pointerA != null)
            {
                pointerA = pointerA.next;
                countA++;
            }
            while (pointerB != null)
            {
                pointerB = pointerB.next;
                countB++;
            }

            pointerA = headA; pointerB = headB;
            if (countA > countB)
            {
                int diff = countA - countB;
                while (diff > 0)
                {
                    pointerA = pointerA.next;
                }
            }
            if (countB > countA)
            {
                int diff = countB - countA;
                while (diff > 0)
                {
                    pointerB = pointerB.next;
                }
            }
            while (pointerA != null && pointerB != null)
            {
                if (pointerA.Equals(pointerB))
                {
                    return pointerA;
                }
                pointerB = pointerB.next;
                pointerA = pointerA.next;
            }


            return null;
        }

        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            //Input: head = [1, 2, 3, 4, 5], n = 2
            //Output:[1,2,3,5]

            if (head == null || head.next == null) return null;

            ListNode listNode = head;
            ListNode nodeToDelete = head, previousNode = head;
            int length = 0;
            while(listNode!=null)
            {
                listNode = listNode.next;
                length++;
            }

            for (int i = 0; i < length-n; i++)
            {
                previousNode = nodeToDelete;
                nodeToDelete = nodeToDelete.next;
            }
            if(nodeToDelete == head)
            {
                return nodeToDelete.next;
            }
            if (nodeToDelete != null && previousNode != null)
            {
                previousNode.next = nodeToDelete.next;
            }
            return head;
        }

        public ListNode ReverseList(ListNode head)
        {
            // head = [1, 2, 3, 4, 5]
            if (head == null) return null;
            ListNode listNode = head, previousNode = head.next;
            
            while (listNode != null && listNode.next!=null)
            {

                listNode.next = previousNode.next;
                previousNode.next = head;

                head = previousNode;

                previousNode = listNode.next;
                
            }

            return head;
        }

        public ListNode RemoveElements(ListNode head, int val)
        {
            if (head ==null)
            {
                return head;
            }
            while (head != null && head.val == val)
            {
                head = head.next;
            }

            ListNode prevNode = head;
            ListNode listNode = head.next;
            while(listNode !=null && prevNode !=null)
            {
                if (listNode.val == val)
                {
                    prevNode.next = listNode.next;
                    listNode = listNode.next;
                }
                prevNode = prevNode.next;
                listNode = listNode.next;
            }

            return head;
        }
        public ListNode OddEvenList(ListNode head)
        {

        }
    }


    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x)
        {
            val = x;
            next = null;
        }
    }
}
