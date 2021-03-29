/**
 * @file inorderTraversal.cpp
 * @author João Neves (https://github.com/joao-neves95)
 * @brief
 *  Traverse the left subtree first. Then visit the root. Finally, traverse the right subtree.
 *
 * @version 1.0.0
 * @copyright Copyright (c) 2021
 * (_SHIVAYL_)
 */
#include <vector>
#include <stack>
using namespace std;

/*
 Definition for a binary tree node.
 */
 struct TreeNode {
     int val;
     TreeNode *left;
     TreeNode *right;
     TreeNode() : val(0), left(nullptr), right(nullptr) {}
     TreeNode(int x) : val(x), left(nullptr), right(nullptr) {}
     TreeNode(int x, TreeNode *left, TreeNode *right) : val(x), left(left), right(right) {}
 };
 /**/

class Solution {
public:
    vector<int> inorderTraversalIterative(TreeNode* root) {
        vector<int> res;

        if (root == nullptr) {
            return res;
        }

        stack<TreeNode*> nodeStack;

        TreeNode* currNode = root;

        while (!nodeStack.empty() || currNode != nullptr) {

            while (currNode != nullptr) {
                nodeStack.push(currNode);
                currNode = currNode->left;
            }

            // Pop left first.
            currNode = nodeStack.top();
            nodeStack.pop();
            res.push_back(currNode->val);

            // Then check right.
            currNode = currNode->right;
        }

        return res;
    }
};

int main(int argc, char const *argv[])
{
    return 0;
}
